using Core.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Base
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(Context dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// get product list all
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductListAsync()
        {
            return await GetAllAsync();
        }

        /// <summary>
        /// get product by sku
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductBySkuAsync(string sku)
        {
            return await GetAsync(x => x.Sku == sku);
        }
     
    }
}
