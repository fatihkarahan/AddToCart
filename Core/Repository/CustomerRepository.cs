using Core.Domain.Entities.Customer;
using Core.Repository.Base;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(Context dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// get customer with cart
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerWithCartsAsync(int customerId)
        {
            var spec = new CustomerWithCartsSpecification(customerId);
            var customer = (await GetAsync(spec)).FirstOrDefault();
            return customer;
        }
    }
}
