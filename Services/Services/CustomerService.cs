using AutoMapper;
using Core.Domain.Entities.Customer;
using Core.Repository;
using Data.Models;
using Services.Mapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CustomerService : ICustomerService
    {
        #region field
        /// <summary>
        /// customer repository
        /// </summary>
        private readonly ICustomerRepository _customerRepository;
        #endregion

        #region cotr
        /// <summary>
        /// cotr
        /// </summary>
        /// <param name="customerRepository"></param>
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }
        #endregion
        #region methods
        /// <summary>
        /// delete customer
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public async Task Delete(CustomerModel productModel)
        {
            var deletedCustomer = await _customerRepository.GetByIdAsync(productModel.Id);
            if (deletedCustomer == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _customerRepository.DeleteAsync(deletedCustomer);
        }

        /// <summary>
        /// get customer by id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<CustomerModel> GetCustomerById(int customerId)
        {
            var customer = await _customerRepository.GetCustomerWithCartsAsync(customerId);
            var mapped = ObjectMapper.Mapper.Map<CustomerModel>(customer);
            return mapped;
        }

        /// <summary>
        /// update customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public async Task Update(CustomerModel customerModel)
        {
            var editCustomer = await _customerRepository.GetByIdAsync(customerModel.Id);
            if (editCustomer == null)
                throw new ApplicationException($"Entity could not be loaded.");

            ObjectMapper.Mapper.Map<CustomerModel, Customer>(customerModel, editCustomer);

            await _customerRepository.UpdateAsync(editCustomer);
        }
        #endregion
    }
}
