namespace Ambev.DeveloperEvaluation.Application.Sale.SaleItem
{
    public class CreateSaleItemCommand
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
