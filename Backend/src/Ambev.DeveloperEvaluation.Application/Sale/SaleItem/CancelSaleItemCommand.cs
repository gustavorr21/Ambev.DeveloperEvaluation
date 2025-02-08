using MediatR;

public record CancelSaleItemCommand(Guid SaleItemId, bool isCancelled) : IRequest;
