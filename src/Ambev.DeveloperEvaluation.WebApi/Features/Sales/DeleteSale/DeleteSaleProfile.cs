using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

public class DeleteSaleProfile : Profile
{
    public DeleteSaleProfile()
    {
        CreateMap<int, Sales>()
            .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => true));
    }
}
