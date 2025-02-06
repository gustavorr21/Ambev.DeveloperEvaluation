using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sale.SaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>()
                .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();

            CreateMap<CreateSaleCommand, SalesEntity>()
               .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
               .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
               .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
               .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => src.TotalValue))
               .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
               .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<SalesEntity, CreateSaleResult>();

            CreateMap<CreateSaleItemCommand, SaleItemsEntity>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));

        }
    }
}
