using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

public class GetAllSalesCommandHandler : IRequestHandler<GetAllSalesCommand, IEnumerable<GetSaleResponse>>
{
    private readonly ISaleService _saleService;

    public GetAllSalesCommandHandler(ISaleService saleService)
    {
        _saleService = saleService;
    }

    public async Task<IEnumerable<GetSaleResponse>> Handle(GetAllSalesCommand request, CancellationToken cancellationToken)
    {
        var sales = await _saleService.GetAllAsync(cancellationToken);

        return sales.Select(sale => new GetSaleResponse
        {
            Id = sale.Id,
            SaleDate = sale.SaleDate,
            Branch = sale.Branch,
            Client = sale.Client,
            IsCancelled = sale.IsCancelled,
            SaleNumber = sale.SaleNumber,
            TotalValue = sale.TotalValue,
            Items = sale.Items.Select(item => new SaleItemResponse
            {
                Id = item.Id,
                Product = item.Product,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList()
        });
    }
}
