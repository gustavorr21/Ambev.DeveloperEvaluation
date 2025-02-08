using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
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

        foreach (var itemRequest in request.Request.Items)
        {
            var existingItem = sale.Items.FirstOrDefault(i => i.Id == itemRequest.Id);

            if (existingItem != null)
            {
                existingItem.Product = itemRequest.Product;
                existingItem.Quantity = itemRequest.Quantity;
                existingItem.UnitPrice = itemRequest.UnitPrice;
            }
            else
            {
                var newItem = new SaleItemsEntity
                {
                    Product = itemRequest.Product,
                    Quantity = itemRequest.Quantity,
                    UnitPrice = itemRequest.UnitPrice,
                };
                sale.Items.Add(newItem);
            }
        }

        await _saleService.UpdateSaleAsync(sale, cancellationToken);

        await _mediator.Publish(new SaleModifiedEvent(request.SaleId), cancellationToken);
    }

}
