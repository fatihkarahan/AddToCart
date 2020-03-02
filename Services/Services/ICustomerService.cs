using Data.Models;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// get customer by id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<CustomerModel> GetCustomerById(int customerId);

        /// <summary>
        /// update to customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        Task Update(CustomerModel customerModel);

        /// <summary>
        /// delete to customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        Task Delete(CustomerModel customerModel);
    }
}
