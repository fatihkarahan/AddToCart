using Core.Domain.Entities.ShoppingCart;
using Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
    }
}
