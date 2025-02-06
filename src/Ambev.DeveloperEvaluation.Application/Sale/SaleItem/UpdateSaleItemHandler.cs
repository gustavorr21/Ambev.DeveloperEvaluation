using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;

public class UpdateSaleItemCommandHandler : IRequestHandler<UpdateSaleItemCommand>
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateSaleItemCommandHandler(ISaleService saleService, IMapper mapper, IMediator mediator)
    {
        _saleService = saleService;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task Handle(UpdateSaleItemCommand request, CancellationToken cancellationToken)
    {
        var saleItem = await _saleService.GetSaleItemByIdAsync(request.SaleItemId, cancellationToken);
        if (saleItem == null) throw new Exception("Item da venda não encontrado.");

        saleItem.Quantity = request.Request.Quantity;
        saleItem.UnitPrice = request.Request.Price;
        saleItem.IsCancelled = request.Request.IsCancelled;

        await _saleService.UpdateSaleItemAsync(saleItem, cancellationToken);

        await _mediator.Publish(new SaleItemModifiedEvent(request.SaleItemId), cancellationToken);
    }
}
