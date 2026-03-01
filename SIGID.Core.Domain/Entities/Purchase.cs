namespace SIGID.Core.Domain.Entities
{
    public class Purchase
    {
        public string Id { get; set; }
        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public ICollection<PurchaseDetail> Details { get; set; }
    }

    public class PurchaseDetail
    {
        public string Id { get; set; }
        public string PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
