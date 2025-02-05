namespace Ambev.DeveloperEvaluation.Application.Sale.SaleItem
{
    public class CreateSaleItemResult
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
