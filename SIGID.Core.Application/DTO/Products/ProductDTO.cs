public class ProductDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public int StockMin { get; set; }
    public int CurrStock { get; set; }
    public string Status { get; set; }
}

public class CreateProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public int StockMin { get; set; }
    public int CurrStock { get; set; }
    public string Status { get; set; }
}

public class UpdateProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public int StockMin { get; set; }
    public int CurrStock { get; set; }
    public string Status { get; set; }
}