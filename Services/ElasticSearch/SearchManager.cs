using Core.Domain.Entities.Product;
using Data.Models;
using Microsoft.Extensions.Configuration;
using Nest;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.ElasticSearch
{
    public class SearchManager : ISearchManager
    {
        #region Fields
        private readonly IElasticClient _client = null;
        private readonly string indexName = "product";
        private readonly string url = "http://localhost:9200/";
        private readonly IProductService _productService;
        #endregion

        #region Constructors 
        public SearchManager(IConfiguration configuration, IProductService productService)
        {
            _productService = productService;
            var settings = new ConnectionSettings(new Uri(url))
             .DefaultIndex(indexName)
             .DefaultMappingFor<ProductModel>(m => m
                 .PropertyName(p => p.Id, "id")
             );

            _client = new ElasticClient(settings);
        }
        #endregion

        #region methods
        /// <summary>
        /// create index elastic search
        /// </summary>
        public void CreateIndex()
        {
            ///index yoksa
            if (!_client.IndexExists(indexName).Exists)
            {
                var createIndexResponse = _client.CreateIndex(indexName, c => c
      .Settings(s => s
          .NumberOfShards(1)
          .NumberOfReplicas(0)
      )
      .Mappings(m => m
          .Map<ProductModel>(d => d
              .AutoMap()
          )
      )
  );

                ////add data
                AddData();
            }
        }

        /// <summary>
        /// delete index 
        /// </summary>
        /// <param name="indexName"></param>
        public void DeleteIndex(string indexName)
        {
            if (_client.IndexExists(indexName).Exists)
            {
                var indexResponse = _client.DeleteIndex(indexName);
            }
        }

        /// <summary>
        /// add default data
        /// </summary>
        public void AddData()
        {
            var productList = _productService.GetProductList();
            foreach (var item in productList.Result)
            {
                var result = _client.Index(item, i => i
                         .Index(indexName)
                         .Type("productmodel")
                         .Id(item.Id)
                         //.Refresh()
                         );
            }
        }

        /// <summary>
        /// search in elastic search
        /// </summary>
        /// <param name="sku"></param>
        /// <returns> product model</returns>
        public ProductModel SearchProduct(string sku)
        {
            ////create index and add data
            CreateIndex();

            var response = _client.Search<ProductModel>(s => s
            .From(0)
            .Size(1)
            .Query(q => q
            .Match(m => m
            .Field(f => f.Sku)
            .Query(sku)
         )));

            return response.Documents.FirstOrDefault();
        }
        #endregion
    }
}
