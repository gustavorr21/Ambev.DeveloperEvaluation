using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly DiscountService _discountService;

        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, DiscountService discountService)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _discountService = discountService;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<SalesEntity>(request);
            sale.SaleDate = sale.SaleDate.ToLocalTime();

            _discountService.ApplyDiscounts(sale);
            sale.TotalValue = sale.Items.Sum(item => item.Quantity * item.UnitPrice);

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            return _mapper.Map<CreateSaleResult>(createdSale);
        }

    }
}
