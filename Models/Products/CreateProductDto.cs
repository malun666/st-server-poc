namespace ST.Server.Models.Products;

public class CreateProductDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
} 