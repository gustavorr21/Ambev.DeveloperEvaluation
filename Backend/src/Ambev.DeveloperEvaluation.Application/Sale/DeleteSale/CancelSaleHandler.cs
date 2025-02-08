    using Ambev.DeveloperEvaluation.Application.Sale.Events;
    using Ambev.DeveloperEvaluation.Domain.Services;
    using MediatR;

    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand>
    {
        private readonly ISaleService _saleService;
        private readonly IMediator _mediator;

        public CancelSaleHandler(ISaleService saleService, IMediator mediator)
        {
            _saleService = saleService;
            _mediator = mediator;
        }

        public async Task Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            await _saleService.CancelSaleAsync(request.SaleId, request.IsCancelled, cancellationToken);
            await _mediator.Publish(new SaleCancelledEvent(request.SaleId), cancellationToken);
        }
    }
