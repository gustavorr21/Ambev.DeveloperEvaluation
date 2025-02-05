using MediatR;

public class CreateSaleCommand : IRequest<CreateSaleResponse>
{
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public string Client { get; set; }
    public decimal TotalValue { get; set; }
    public string Branch { get; set; }
}
