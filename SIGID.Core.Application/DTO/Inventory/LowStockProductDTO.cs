namespace SIGID.Core.Application.DTO.Inventory
{
    public class LowStockProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
        public int StockDifference { get; set; }
        public string Status { get; set; }
    }
}
