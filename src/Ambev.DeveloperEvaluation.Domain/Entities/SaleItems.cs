namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItems
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid SaleId { get; set; }
        public Sales Sale { get; set; }
    }
}
