using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<SalesEntity, GetSaleResponse>();
        CreateMap<SaleItemsEntity, SaleItemResponse>();
    }
}
