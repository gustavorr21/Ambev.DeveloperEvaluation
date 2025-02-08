public class UpdateSaleRequest
{
    public string SaleNumber { get; set; }
    public string Client { get; set; }
    public string Branch { get; set; }
    public decimal TotalValue { get; set; }
    public bool IsCancelled { get; set; }
    public List<SaleItemRequest> Items { get; set; }
}

public class SaleItemRequest
{
    public Guid? Id { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
