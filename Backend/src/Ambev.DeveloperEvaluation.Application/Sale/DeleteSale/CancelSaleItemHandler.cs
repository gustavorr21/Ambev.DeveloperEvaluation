using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand>
{
    private readonly ISaleService _saleService;
    private readonly IMediator _mediator;

    public CancelSaleItemHandler(ISaleService saleService, IMediator mediator)
    {
        _saleService = saleService;
        _mediator = mediator;
    }

    public async Task Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        await _saleService.CancelSaleItemAsync(request.SaleItemId, cancellationToken);
        await _mediator.Publish(new SaleItemCancelledEvent(request.SaleItemId), cancellationToken);
    }
}
