
namespace ProductManagement.Api.Models;

public class Product
{
    public Int32 Id { get; set; }
    public String Name { get; set; } = String.Empty;
    public String Description { get; set; } = String.Empty;
    public Decimal Price { get; set; }
    public DateTime DateCreation { get; set; }
}
