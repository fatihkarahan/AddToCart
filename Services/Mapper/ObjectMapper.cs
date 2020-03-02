using AutoMapper;
using Core.Domain.Entities.Customer;
using Core.Domain.Entities.Product;
using Core.Domain.Entities.ShoppingCart;
using Data.Models;

namespace Services.Mapper
{
    // The best implementation of AutoMapper for class libraries - https://stackoverflow.com/questions/26458731/how-to-configure-auto-mapper-in-class-library-project
    public class ObjectMapper
    {
        public static IMapper Mapper
        {
            get
            {
                return AutoMapper.Mapper.Instance;
            }
        }
        static ObjectMapper()
        {
            CreateMap();
        }

        private static void CreateMap()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {

                cfg.CreateMap<Product, ProductModel>()
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                    .ReverseMap();
                cfg.CreateMap<Customer, ShoppingCart>()
                 .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
                cfg.CreateMap<Customer, CustomerModel>()
             .ReverseMap();
                ///automapper error fix https://stackoverflow.com/questions/27359999/auto-mapper-unmapped-members-were-found
                cfg.ValidateInlineMaps = false;
            });
        }
    }
}
