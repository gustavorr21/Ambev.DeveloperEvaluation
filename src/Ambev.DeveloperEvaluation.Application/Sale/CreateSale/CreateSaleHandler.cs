﻿using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;
        private readonly DiscountService _discountService;
        private readonly IMediator _mediator;

        public CreateSaleHandler(
            ISaleService saleService,
            IMapper mapper, 
            DiscountService discountService,
            IMediator mediator)
        {
            _saleService = saleService;
            _mapper = mapper;
            _discountService = discountService;
            _mediator = mediator;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<SalesEntity>(request);
            sale.SaleDate = sale.SaleDate.ToLocalTime();

            _discountService.ApplyDiscounts(sale);
            sale.TotalValue = sale.Items.Sum(item => item.Quantity * item.UnitPrice);

            var createdSale = await _saleService.CreateAsync(sale, cancellationToken);

            return _mapper.Map<CreateSaleResult>(createdSale);
        }

    }
}
