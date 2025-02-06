using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand>
{
    private readonly ISaleService _saleService;
    private readonly IMediator _mediator;

    public UpdateSaleHandler(ISaleService saleService, IMediator mediator)
    {
        _saleService = saleService;
        _mediator = mediator;
    }

    public async Task Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleService.GetByIdAsync(request.SaleId, cancellationToken);
        if (sale == null) throw new Exception("Venda não encontrada.");

        sale.SaleNumber = request.Request.SaleNumber;
        sale.Client = request.Request.Client;
        sale.Branch = request.Request.Branch;
        sale.TotalValue = request.Request.TotalValue;
        sale.IsCancelled = request.Request.IsCancelled;

        await _saleService.UpdateSaleAsync(sale, cancellationToken);
        await _mediator.Publish(new SaleModifiedEvent(request.SaleId), cancellationToken);
    }
}
