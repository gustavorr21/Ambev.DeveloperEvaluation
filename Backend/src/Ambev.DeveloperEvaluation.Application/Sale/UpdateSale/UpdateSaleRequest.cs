public class UpdateSaleRequest
{
    public string SaleNumber { get; set; }
    public string Client { get; set; }
    public string Branch { get; set; }
    public decimal TotalValue { get; set; }
    public bool IsCancelled { get; set; }
}
