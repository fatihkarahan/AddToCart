using Core.Domain.Entities.ShoppingCart;
using Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart> , IShoppingCartRepository
    {
        public ShoppingCartRepository(Context dbContext) : base(dbContext)
        {
        }
    }
}
