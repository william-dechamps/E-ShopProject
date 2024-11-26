namespace EShopProject.Dtos;

public class AddProductDto
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Image { get; set; }
    public required string Category { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public required string InternalReference { get; set; }
    public int ShellId { get; set; }
    public int Rating { get; set; }
    public required string InventoryStatus { get; set; }
}