using MediatR;

public record CancelSaleCommand(Guid SaleId) : IRequest;
