using Services.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    /// <summary>
    /// product model
    /// </summary>
    public class ProductModel : BaseModel
    {
        /// <summary>
        /// prouct name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// prouct sku
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// prouct stock quantity
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// prouct max saleable stock quantity
        /// </summary>
        public int MaxSaleableQuantity { get; set; }

        /// <summary>
        /// product is active 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// product is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

    }
}
