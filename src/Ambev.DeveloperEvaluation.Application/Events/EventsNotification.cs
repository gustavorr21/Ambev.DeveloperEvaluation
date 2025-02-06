using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events
{
    public record SaleCreatedEvent(Guid SaleId) : INotification;
    public record SaleModifiedEvent(Guid SaleId) : INotification;
    public record SaleCancelledEvent(Guid SaleId) : INotification;
    public record ItemCancelledEvent(Guid SaleId, Guid ItemId) : INotification;
    public record SaleItemCancelledEvent(Guid SaleId) : INotification;
}
