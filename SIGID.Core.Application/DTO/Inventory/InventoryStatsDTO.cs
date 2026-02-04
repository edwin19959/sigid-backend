namespace SIGID.Core.Application.DTO.Inventory
{
    public class InventoryStatsDTO
    {
        public int? TotalProducts { get; set; }
        public int? TotalInventoryValue { get; set; }
        public int? LowStockProducts { get; set; }
    }
}
