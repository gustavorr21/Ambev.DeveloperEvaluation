using MediatR;
using System;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Application.Sale.SaleItem;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Client { get; set; }
        public decimal TotalValue { get; set; }
        public string Branch { get; set; }
        public List<CreateSaleItemCommand> Items { get; set; }
    }
}
