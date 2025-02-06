using MediatR;

public record UpdateSaleCommand(Guid SaleId, UpdateSaleRequest Request) : IRequest;
