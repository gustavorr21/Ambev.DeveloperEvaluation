using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;

public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResponse>
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public GetSaleHandler(ISaleService saleService, IMapper mapper)
    {
        _saleService = saleService;
        _mapper = mapper;
    }

    public async Task<GetSaleResponse> Handle(GetSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleService.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
        {
            return null;
        }

        return _mapper.Map<GetSaleResponse>(sale);
    }
}
