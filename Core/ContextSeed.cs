using Core.Domain.Entities.Customer;
using Core.Domain.Entities.Product;
using Data.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ContextSeed
    {
        /// <summary>
        /// add mock data sql memory
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        public static async Task SeedAsync(Context dbContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // TODO: Only run this if using a real database
                // dbContext.Database.Migrate();
                 dbContext.Database.EnsureCreated();
                if (!dbContext.Customer.Any())
                {
                    dbContext.Customer.AddRange(GetPreconfiguredCustomer());
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Product.Any())
                {
                    dbContext.Product.AddRange(GetPreconfiguredProducts());
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<ContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(dbContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Customer> GetPreconfiguredCustomer()
        {
            return new List<Customer>()
            {
                new Customer() {Email = "sfatihkarahan@gmail.com",Password = "12345",FullName = "fatih karahan" , IsActive = true , IsDeleted = false ,CreatedDate = DateTime.Now,UpdatedDate = null,ShoppingCarts = new List<Domain.Entities.ShoppingCart.ShoppingCart>(){
               new Domain.Entities.ShoppingCart.ShoppingCart()
               {
                   CustomerId = 1,ProductId = 1 ,Quantity = 1 ,CreatedDate = DateTime.Now ,CartType = (int)ShoppingCartType.Cart,UpdatedDate = null
               }
                } },
                
            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product() {  Name = "100 lü gül", Sku = "1000", IsActive = true, IsDeleted =false , StockQuantity = 10, MaxSaleableQuantity = 5 ,CreatedDate = DateTime.Now },
                new Product() {  Name = "1 gül", Sku = "1001", IsActive = true, IsDeleted = false , StockQuantity = 5, MaxSaleableQuantity =1 ,CreatedDate = DateTime.Now },
                new Product() {  Name = "5 gül", Sku = "10002", IsActive = true, IsDeleted = true , StockQuantity = 100, MaxSaleableQuantity = 10 ,CreatedDate = DateTime.Now },

            };
        }
    }
}
