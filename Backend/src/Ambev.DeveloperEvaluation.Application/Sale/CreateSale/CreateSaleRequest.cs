public class CreateSaleRequest
{
    public string SaleNumber { get; set; }
    public string Client { get; set; }
    public string Branch { get; set; }
    public List<CreateSaleItemRequest> Items { get; set; }
}

public class CreateSaleItemRequest
{
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
