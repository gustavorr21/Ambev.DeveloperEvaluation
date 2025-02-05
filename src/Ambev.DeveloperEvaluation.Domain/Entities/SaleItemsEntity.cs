namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItemsEntity
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid SaleId { get; set; }
        public SalesEntity Sale { get; set; }
    }
}
