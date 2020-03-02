using AutoMapper;
using Core.Domain.Entities.ShoppingCart;
using Core.Repository;
using Data.Models;
using Services.Mapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        #region field
        /// <summary>
        /// shopping cart repo
        /// </summary>
        private readonly IShoppingCartRepository _shoppingCartRepository;
        #endregion
        #region cont
        /// <summary>
        /// cotr
        /// </summary>
        /// <param name="shoppingCartRepository"></param>
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        }
        #endregion
        #region methods
        /// <summary>
        /// add to shopping cart
        /// </summary>
        /// <param name="shoppingCartModel"></param>
        /// <returns></returns>
        public async Task Add(ShoppingCartModel shoppingCartModel)
        {
            ShoppingCart model = new ShoppingCart();
            ObjectMapper.Mapper.Map<ShoppingCartModel, ShoppingCart>(shoppingCartModel, model);

           var resp = await _shoppingCartRepository.AddAsync(model);
        }

        /// <summary>
        /// update to cart
        /// </summary>
        /// <param name="shoppingCartModel"></param>
        /// <returns></returns>
        public async Task Update(ShoppingCartModel shoppingCartModel)
        {
            var editCart = await _shoppingCartRepository.GetByIdAsync(shoppingCartModel.Id);
            if (editCart == null)
                throw new ApplicationException($"Entity could not be loaded.");

            ObjectMapper.Mapper.Map<ShoppingCartModel, ShoppingCart>(shoppingCartModel, editCart);

            await _shoppingCartRepository.UpdateAsync(editCart);
        }
        #endregion
    }
}
