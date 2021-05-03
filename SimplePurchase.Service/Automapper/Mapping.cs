using AutoMapper;
using SimpleApplication.Domain.Models;
using SimplePurchase.Service.Models;
using SimplePurchase.Service.Models.Store;
using System;

namespace SimplePurchase.Service.Automapper
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductEntity, ProductViewModel>().ForMember(o => o.ProductId, b => b.MapFrom(z => z.Id));
            CreateMap<ProductEntity, ProductModel>().ForMember(o => o.ProductId, b => b.MapFrom(z => z.Id));
            CreateMap<ProductModel, ProductEntity>().ForMember(o => o.Id, b => b.MapFrom(z => z.ProductId));
            CreateMap<PurchaseEntity, PurchaseModel>();
            CreateMap<PurchaseModel, PurchaseEntity>();
        }
    }
}
