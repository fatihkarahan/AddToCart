using ApiCustomer.Middleware;
using Core;
using Core.Configuration;
using Core.Repository;
using Core.Repository.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Services.ElasticSearch;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ApiCustomer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //// up services
            ConfigureRunServices(services);

            services.AddControllers();
        }

        /// <summary>
        /// add services
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureRunServices(IServiceCollection services)
        {
            //// Add Core Layer
            services.Configure<ContextRunSettings>(Configuration);

            // Add Infrastructure Layer
            ConfigureDatabases(services);
            ////swagger
            services.AddSwaggerGen(c =>
            {
                //// sepcify our operation filter here.  
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = $"v1 API",
                    Description = "v1 API",
                    TermsOfService = new Uri("https://www.linkedin.com/in/fatihkarahan/"),
                    Contact = new OpenApiContact
                    {
                        Name = "fatih karahan",
                        Email = "sfatihkarahan@gmail.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache-2.0",
                        Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                });
            });
            services.AddMemoryCache();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddHttpContextAccessor();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            //// for elastic search
            services.AddScoped<ISearchManager, SearchManager>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            ////middleware inject
            app.UseWebAPILogger();
            ////serilog added
            loggerFactory.AddSerilog();
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c =>
            {
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 API");
            });
        }

        /// <summary>
        /// database add
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDatabases(IServiceCollection services)
        {
            ////use in-memory database
            services.AddDbContext<Context>(c =>
                c.UseInMemoryDatabase("ecomm"));

            //// use real database
            //services.AddDbContext<AspnetRunContext>(c =>
            //    c.UseSqlServer(Configuration.GetConnectionString("AspnetRunConnection")));
        }
    }
}
