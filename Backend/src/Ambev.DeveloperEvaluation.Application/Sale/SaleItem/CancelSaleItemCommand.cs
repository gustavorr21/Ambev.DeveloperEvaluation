using MediatR;

public record CancelSaleItemCommand(Guid SaleItemId) : IRequest;
