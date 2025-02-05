using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.DeleteSale
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        public Guid SaleId { get; set; }
    }
}
