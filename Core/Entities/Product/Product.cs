using Core.Entities.Base;
using System;

namespace Core.Domain.Entities.Product
{
    /// <summary>
    /// product model
    /// </summary>
    public class Product : Entity
    {
        /// <summary>
        /// product name
        /// </summary>
        public string Name { get; set; }

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

        /// <summary>
        /// product created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

    }
}
