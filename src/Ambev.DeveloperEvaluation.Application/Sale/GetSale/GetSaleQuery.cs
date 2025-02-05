using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale
{
    public class GetSaleQuery : IRequest<GetSaleResult>
    {
        public Guid SaleId { get; set; }
    }
}
