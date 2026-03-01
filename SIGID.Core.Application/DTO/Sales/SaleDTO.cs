namespace SIGID.Core.Application.DTO.Sales
{
    public class SaleDTO
    {
        public string Id { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public List<SaleDetailDTO> Details { get; set; }
    }

    public class SaleDetailDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class CreateSaleDTO
    {
        public string ClientName { get; set; }
        public string Status { get; set; }
        public List<CreateSaleDetailDTO> Details { get; set; }
    }

    public class CreateSaleDetailDTO
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
