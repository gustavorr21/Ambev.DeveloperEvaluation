using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Sales, GetSaleResponse>();
    }
}
