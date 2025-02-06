using MediatR;

public record UpdateSaleItemCommand(Guid SaleItemId, UpdateSaleItemRequest Request) : IRequest;
