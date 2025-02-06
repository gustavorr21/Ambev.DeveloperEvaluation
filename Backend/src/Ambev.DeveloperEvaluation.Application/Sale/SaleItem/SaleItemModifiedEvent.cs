using MediatR;

public record SaleItemModifiedEvent(Guid SaleItemId) : INotification;
