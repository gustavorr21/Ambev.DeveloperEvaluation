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

        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            // Mapeia de CreateSaleCommand para SalesEntity
            var sale = _mapper.Map<SalesEntity>(request);
            sale.SaleDate = sale.SaleDate.ToLocalTime();

            // Cria a venda
            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            // Mapeia de SalesEntity para CreateSaleResult
            return _mapper.Map<CreateSaleResult>(createdSale);
        }

    }
}
