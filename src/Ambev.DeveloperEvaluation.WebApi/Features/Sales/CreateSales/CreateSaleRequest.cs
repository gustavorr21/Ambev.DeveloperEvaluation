namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales
{
    public class CreateSaleRequest
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Client { get; set; }
        public decimal TotalValue { get; set; }
        public string Branch { get; set; }
        public List<SaleItemRequest> Items { get; set; }
    }

    public class SaleItemRequest
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
