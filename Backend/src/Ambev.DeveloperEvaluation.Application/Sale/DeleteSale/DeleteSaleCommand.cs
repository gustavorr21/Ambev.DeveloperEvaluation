using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.DeleteSale
{
    public class DeleteSaleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
