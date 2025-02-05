namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sales
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Client { get; set; }
        public decimal TotalValue { get; set; }
        public string Branch { get; set; }
        public bool IsCancelled { get; set; }

        public List<SaleItems> Items { get; set; }
    }
}
