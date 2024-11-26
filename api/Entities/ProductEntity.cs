namespace EShopProject.Entities;
using System.ComponentModel.DataAnnotations;

public enum InventoryStatus
{
  [Display(Name = "INSTOCK")]
  INSTOCK,
  [Display(Name = "LOWSTOCK")]
  LOWSTOCK,
  [Display(Name = "OUTOFSTOCK")]
  OUTOFSTOCK
}

public class ProductEntity : IEntityId
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
  public InventoryStatus InventoryStatus { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}