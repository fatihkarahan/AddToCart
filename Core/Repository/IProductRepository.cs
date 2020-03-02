using Core.Domain.Entities.Product;
using Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IProductRepository : IRepository<Product>
    {

        Task<IEnumerable<Product>> GetProductListAsync();
        Task<IEnumerable<Product>> GetProductBySkuAsync(string sku);
    }
}
