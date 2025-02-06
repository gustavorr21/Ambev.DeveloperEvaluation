using MediatR;

public class GetSaleCommand : IRequest<GetSaleResponse>
{
    public Guid Id { get; set; }
}
