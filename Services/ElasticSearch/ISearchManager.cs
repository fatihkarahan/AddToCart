using Core.Domain.Entities.Product;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ElasticSearch
{
    public interface ISearchManager
    {
        /// <summary>
        /// create index elastic search
        /// </summary>
        void CreateIndex();

        /// <summary>
        /// delete index 
        /// </summary>
        /// <param name="indexName"></param>
        void DeleteIndex(string indexName);

        /// <summary>
        /// add default data
        /// </summary>
        void AddData();

        /// <summary>
        /// search in elastic search
        /// </summary>
        /// <param name="sku"></param>
        /// <returns> product model</returns>
        ProductModel SearchProduct(string sku);
    }
}
