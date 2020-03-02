using Services.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class CustomerModel : BaseModel
    {
        /// <summary>
        /// customer full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// customer email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// customer password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// customer is active or passive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// customer is deleted
        /// </summary>
        public bool IsDeleted { get; set; }


        /// <summary>
        /// shopping cart item
        /// </summary>
        public List<ShoppingCartModel> ShoppingCarts { get; set; }
    }
}
