using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

public class DeleteSaleProfile : Profile
{
    public DeleteSaleProfile()
    {
        CreateMap<int, SalesEntity>()
            .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => true));
    }
}
