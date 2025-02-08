using MediatR;

public class GetAllSalesCommand : IRequest<IEnumerable<GetSaleResponse>>
{
}
