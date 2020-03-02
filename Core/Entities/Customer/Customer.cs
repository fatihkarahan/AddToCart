using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;
namespace Core.Domain.Entities.Customer
{
    /// <summary>
    /// customer model
    /// </summary>
    public class Customer : Entity
    {
        public Customer()
        {
            ShoppingCarts = new HashSet<ShoppingCart.ShoppingCart>();
        }
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
        /// customer created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// customer is active or passive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// customer is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// customer updated date
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// shopping cart item
        /// </summary>
        public ICollection<ShoppingCart.ShoppingCart> ShoppingCarts { get; set; }

    }
}
