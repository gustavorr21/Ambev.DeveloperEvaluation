//using MediatR;
//using Ambev.DeveloperEvaluation.Domain.Repositories;
//using Ambev.DeveloperEvaluation.Domain.Entities;
//using AutoMapper;

//namespace Ambev.DeveloperEvaluation.Application.Sale.SaleItem
//{
//    public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
//    {
//        private readonly ISaleRepository _saleRepository;
//        private readonly IMapper _mapper;

//        public CreateSaleItemHandler(ISaleRepository saleRepository, IMapper mapper)
//        {
//            _saleRepository = saleRepository;
//            _mapper = mapper;
//        }

//        public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand request, CancellationToken cancellationToken)
//        {
//            // Lógica para criar o item da venda
//            var saleItem = _mapper.Map<SaleItemsEntity>(request);
//            var createdSaleItem = await _saleRepository.CreateSaleItemAsync(saleItem, cancellationToken);

//            return _mapper.Map<CreateSaleItemResult>(createdSaleItem);
//        }
//    }
//}
