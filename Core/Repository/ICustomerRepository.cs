using Core.Domain.Entities.Customer;
using Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        /// <summary>
        /// get customer with shopping cart
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<Customer> GetCustomerWithCartsAsync(int customerId);


    }
}
