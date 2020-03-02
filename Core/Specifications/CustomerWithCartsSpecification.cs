using Core.Domain.Entities.Customer;
using Core.Domain.Entities.ShoppingCart;
using Core.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public sealed class CustomerWithCartsSpecification : BaseSpecification<Customer>
    {
        public CustomerWithCartsSpecification(int customerId)
            : base(b => b.Id == customerId)
        {
            AddInclude(b => b.ShoppingCarts);
        }
    }
}
