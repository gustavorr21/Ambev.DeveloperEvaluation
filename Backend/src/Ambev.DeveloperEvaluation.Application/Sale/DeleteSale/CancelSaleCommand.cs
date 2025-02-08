using MediatR;

public record CancelSaleCommand(Guid SaleId, bool isActive) : IRequest;
