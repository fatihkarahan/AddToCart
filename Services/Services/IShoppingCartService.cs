using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IShoppingCartService
    {
        /// <summary>
        /// add cart
        /// </summary>
        /// <param name="shoppingCartModel"></param>
        /// <returns></returns>
        Task Add(ShoppingCartModel shoppingCartModel);
        
        /// <summary>
        /// update cart
        /// </summary>
        /// <param name="shoppingCartModel"></param>
        /// <returns></returns>
        Task Update(ShoppingCartModel shoppingCartModel);
    }
}
