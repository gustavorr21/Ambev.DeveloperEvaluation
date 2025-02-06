using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sale.Events.Handlers
{
    public class SaleEventHandler :
        INotificationHandler<SaleCreatedEvent>,
        INotificationHandler<SaleModifiedEvent>,
        INotificationHandler<SaleCancelledEvent>,
        INotificationHandler<ItemCancelledEvent>
    {
        private readonly ILogger<SaleEventHandler> _logger;

        public SaleEventHandler(ILogger<SaleEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"SaleCreated: Sale {notification.SaleId} was created.");
            return Task.CompletedTask;
        }

        public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"SaleModified: Sale {notification.SaleId} was modified.");
            return Task.CompletedTask;
        }

        public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"SaleCancelled: Sale {notification.SaleId} was cancelled.");
            return Task.CompletedTask;
        }

        public Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"ItemCancelled: Item {notification.ItemId} from Sale {notification.SaleId} was cancelled.");
            return Task.CompletedTask;
        }
        public Task Handle(SaleItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"SubItemCancelled: Sale {notification.SaleId} was cancelled.");
            return Task.CompletedTask;
        }
    }
}
