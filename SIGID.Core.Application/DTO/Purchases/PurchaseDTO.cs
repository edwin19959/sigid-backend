namespace SIGID.Core.Application.DTO.Purchases
{
    public class PurchaseDTO
    {
        public string Id { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public List<PurchaseDetailDTO> Details { get; set; }
    }

    public class PurchaseDetailDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class CreatePurchaseDTO
    {
        public string SupplierId { get; set; }
        public string Status { get; set; }
        public List<CreatePurchaseDetailDTO> Details { get; set; }
    }

    public class CreatePurchaseDetailDTO
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
