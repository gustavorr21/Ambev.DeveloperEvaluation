public class UpdateSaleItemRequest
{
    public string Product { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int Discount { get; set; }
    public bool IsCancelled { get; set; }
}
