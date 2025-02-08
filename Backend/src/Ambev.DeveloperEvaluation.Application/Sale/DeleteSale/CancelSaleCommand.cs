using MediatR;

public record CancelSaleCommand(Guid SaleId, bool IsCancelled) : IRequest;
