using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using ApiCustomer.Model;
using Data.Enums;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Services.ElasticSearch;
using Services.Services;

namespace ApiCustomer.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        #region field
        /// <summary>
        /// search service elastic inject
        /// </summary>
        private readonly ISearchManager _searchManager;

        /// <summary>
        /// search service inject
        /// </summary>
        private readonly IProductService _productService;

        /// <summary>
        /// customer service inject
        /// </summary>
        private readonly ICustomerService _customerService;

        /// <summary>
        /// shopping cart service inject
        /// </summary>
        private readonly IShoppingCartService _shoppingCartService;

        /// <summary>
        /// cache inject
        /// </summary>
        private IMemoryCache cache;

        /// <summary>
        /// customer cache key
        /// </summary>
        private string cacheKey = "cachedCustomerId={0}";

        /// <summary>
        /// customer cache key
        /// </summary>
        static bool enableELK = Convert.ToBoolean(ConfigurationManager.AppSettings["elasticsearch:enabled"]);
        #endregion

        #region ctor
        public CustomerController(
            ISearchManager searchManager,
            IProductService productService,
            ICustomerService customerService,
            IShoppingCartService shoppingCartService,
            IMemoryCache cache
            )
        {
            _searchManager = searchManager;
            _productService = productService;
            _customerService = customerService;
            _shoppingCartService = shoppingCartService;
            this.cache = cache;
        }
        #endregion

        #region method
        /// <summary>
        /// get example data for add or update to cart method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Tuple<IEnumerable<ProductModel>, CustomerModel> GetExampleData()
        {
            var product = _productService.GetProductList();
            var customer = _customerService.GetCustomerById(1);
            return new Tuple<IEnumerable<ProductModel>, CustomerModel>(product.Result, customer.Result);
        }

        /// <summary>
        /// add or update to shopping cart
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Customer/AddOrUpdateCart
        ///     {        
        ///       "customerId": 1,
        ///       "sku": "1000",
        ///       "quantity": 1
        ///     }
        /// </remarks>
        /// <param name="customerId"></param>
        /// <param name="sku"></param>
        /// <param name="quantity"></param>
        /// <returns>A newly created employee</returns>
        /// <response code="200">Returns the newly created or update  item</response>
        /// <response code="400">exception</response>    
        // POST: api/Customer
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ApiResultModel<bool> AddOrUpdateCart(int customerId, string sku, int quantity)
        {
            ////validate params
            ValidateParams(customerId, sku, quantity, out ApiResultModel<bool> result);
            if (result.Status == ResultStatusEnum.ValidationError)
            {
                return result;
            }

            ////validate customer data
            var customer = ValidateCustomer(customerId, out result);

            if (result.Status == ResultStatusEnum.ValidationError)
            {
                return result;
            }

            ////validate product data
            var product = ValidateProduct(sku, quantity, out result);

            if (result.Status == ResultStatusEnum.ValidationError)
            {
                return result;
            }

            ////eğer sepette varsa ürün
            var isAddedBefore = customer.ShoppingCarts.Any(x => x.ProductId == product.Id);
            ////stock check or update cart
            if (isAddedBefore)
            {
                var shoppingCartProduct = customer.ShoppingCarts.FirstOrDefault(x => x.ProductId == product.Id);
                if (shoppingCartProduct.Quantity + quantity > product.StockQuantity)
                {
                    result.Message = "Yeterli stok bulunmuyor.";
                    return result;
                }
                if (shoppingCartProduct.Quantity + quantity > product.MaxSaleableQuantity)
                {
                    result.Message = "Yeterli stok bulunmuyor.";
                    return result;
                }
                ////update cart
                shoppingCartProduct.Quantity += quantity;
                shoppingCartProduct.UpdatedDate = DateTime.Now;
                _shoppingCartService.Update(shoppingCartProduct);
                ////for cache 
                customer.ShoppingCarts.FirstOrDefault(x => x.ProductId == product.Id).Quantity += quantity;
                customer.ShoppingCarts.FirstOrDefault(x => x.ProductId == product.Id).UpdatedDate = DateTime.Now;
            }
            else
            {
                ////add to cart
                _shoppingCartService.Add(new ShoppingCartModel { ProductId = product.Id, CartType = (int)ShoppingCartType.Cart, CustomerId = customerId, Quantity = quantity, CreatedDate = DateTime.Now });
                ////for cache
                customer.ShoppingCarts.Add(
                    new ShoppingCartModel { ProductId = product.Id, CartType = (int)ShoppingCartType.Cart, CustomerId = customerId, Quantity = quantity, CreatedDate = DateTime.Now });
            }

            ////cache set
            var cacheExpirationOptions =
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };
            cache.Set(cacheKey, customer, cacheExpirationOptions);

            result.Message = "Eklendi";
            result.Status = ResultStatusEnum.Success;
            return result;
        }

        /// <summary>
        /// validate post params
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="sku"></param>
        /// <param name="quantity"></param>
        /// <param name="result"></param>
        private void ValidateParams(int customerId, string sku, int quantity, out ApiResultModel<bool> result)
        {
            result = new ApiResultModel<bool>() { Status = ResultStatusEnum.UnSuccess };
            if (customerId == 0)
            {
                result.Status = ResultStatusEnum.ValidationError;
                result.Message = "Giriş yapılması gerekiyor";
                return;
            }

            if (string.IsNullOrEmpty(sku))
            {
                result.Status = ResultStatusEnum.ValidationError;
                result.Message = "Ürün bulunamadı.";
                return;
            }

            if (quantity == 0)
            {
                result.Status = ResultStatusEnum.ValidationError;
                result.Message = "Adet giriniz.";
                return;
            }
        }

        /// <summary>
        /// customer data validate
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private CustomerModel ValidateCustomer(int customerId, out ApiResultModel<bool> result)
        {
            result = new ApiResultModel<bool>() { Status = ResultStatusEnum.UnSuccess };
            cacheKey = string.Format(cacheKey, customerId);

            if (!cache.TryGetValue(cacheKey, out CustomerModel customer))
            {
                if (customer == null)
                {
                    var customerServiceModel = _customerService.GetCustomerById(customerId);
                    customer = (CustomerModel)customerServiceModel.Result;
                }
            }

            if (customer == default(CustomerModel))
            {
                result.Status = ResultStatusEnum.ValidationError;
                result.Message = "Giriş yapılması gerekiyor";
                return customer;
            }

            if (!customer.IsActive || customer.IsDeleted)
            {
                result.Status = ResultStatusEnum.ValidationError;
                result.Message = "Üyeliğiniz bulunamamıştır.";
                return customer;
            }

            return customer;
        }

        /// <summary>
        /// validata product data
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="quantity"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private ProductModel ValidateProduct(string sku, int quantity, out ApiResultModel<bool> result)
        {
            result = new ApiResultModel<bool>() { Status = ResultStatusEnum.UnSuccess };
            ProductModel productModel = new ProductModel();
            if (enableELK)
            {
                productModel = _searchManager.SearchProduct(sku);
            }
            else
            {
                var product = _productService.GetProductBySku(sku);

                if (product.Status != TaskStatus.RanToCompletion)
                {
                    result.Status = ResultStatusEnum.ValidationError;
                    result.Message = "Ürün bulunamadı.";
                    return (ProductModel)product.Result;
                }


                if (product.Result == null || product == default(Task<ProductModel>))
                {
                    result.Status = ResultStatusEnum.ValidationError;
                    result.Message = "Ürün bulunamadı.";
                    return (ProductModel)product.Result;
                }

                productModel = (ProductModel)product.Result;
            }

            if (!productModel.IsActive || productModel.IsDeleted)
            {
                result.Status = ResultStatusEnum.ValidationError;
                result.Message = "Ürün bulunamadı.";
                return productModel;
            }

            if (quantity > productModel.StockQuantity)
            {
                result.Message = "Yeterli stok bulunmuyor.";
                return productModel;
            }
            if (quantity > productModel.MaxSaleableQuantity)
            {
                result.Message = "Yeterli stok bulunmuyor.";
                return productModel;
            }


            return productModel;
        }
        #endregion
    }
}
