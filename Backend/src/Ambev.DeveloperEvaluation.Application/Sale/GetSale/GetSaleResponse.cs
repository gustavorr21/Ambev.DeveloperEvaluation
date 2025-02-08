public class GetSaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public string Client { get; set; }
    public decimal TotalValue { get; set; }
    public string Branch { get; set; }
    public bool IsCancelled { get; set; }
    public List<SaleItemResponse> Items { get; set; }
}

public class SaleItemResponse
{
    public Guid Id { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
}
