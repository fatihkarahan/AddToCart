using ApiCustomer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Pages
{
    public class CustomerPageTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        #region field
        /// <summary>
        /// client 
        /// </summary>
        public HttpClient client { get; }
        #endregion

        #region cotr
        /// <summary>
        /// cotr
        /// </summary>
        /// <param name="factory"></param>
        public CustomerPageTest(CustomWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }
        #endregion

        /// <summary>
        /// add to cart test
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_Example_Data()
        {
            var response = await client.GetAsync("https://localhost:44377/api/Customer/GetExampleData");
            response.EnsureSuccessStatusCode();
            var stringResponse = response.Content.ReadAsStringAsync();

            //assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// add to cart test
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Add_To_Cart()
        {
            var response = await client.PostAsync("https://localhost:44377", new StringContent("/api/Customer/AddShoppingCart?customerId=1&sku=1001&quantity=1", Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var stringResponse = response.Content.ReadAsStringAsync();

            //assert
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
