using AutoMapper;
using Core.Repository;
using Data.Models;
using Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    // TODO : add validation , authorization, logging, exception handling etc. -- cross cutting activities in here.
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <summary>
        /// get product all 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductModel>> GetProductList()
        {
            var productList = await _productRepository.GetProductListAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductModel>>(productList);
            return mapped;
        }

        /// <summary>
        /// get product by sku
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public async Task<ProductModel> GetProductBySku(string sku)
        {
            var product = await _productRepository.GetProductBySkuAsync(sku);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductModel>>(product);
            return mapped.FirstOrDefault();
        }
    }
}
