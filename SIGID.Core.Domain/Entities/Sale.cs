namespace SIGID.Core.Domain.Entities
{
    public class Sale
    {
        public string Id { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public ICollection<SaleDetail> Details { get; set; }
    }

    public class SaleDetail
    {
        public string Id { get; set; }
        public string SaleId { get; set; }
        public Sale Sale { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
