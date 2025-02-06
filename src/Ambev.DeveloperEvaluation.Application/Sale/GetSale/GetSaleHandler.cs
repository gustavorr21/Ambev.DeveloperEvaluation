using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<GetSaleResponse> Handle(GetSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
        {
            return null;
        }

   
        return _mapper.Map<GetSaleResponse>(sale);
    }
}
