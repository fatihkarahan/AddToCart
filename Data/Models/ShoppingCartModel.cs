using Services.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;


namespace Data.Models
{
    /// <summary>
    /// shopping cart model
    /// </summary>
    public class ShoppingCartModel : BaseModel
    {
        /// <summary>
        /// shopping cart type enum 
        /// </summary>
        public int CartType { get; set; }

        /// <summary>
        /// product table product id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// customer table customer id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// product quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// added date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// updated date
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
