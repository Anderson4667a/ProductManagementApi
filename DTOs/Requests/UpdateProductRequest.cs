using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Api.DTOs.Requests;

public class UpdateProductRequest
{
    [Required]
    [MaxLength(100)]
    public String Name { get; set; } = String.Empty;

    [Required]
    [MaxLength(500)]
    public String Description { get; set; } = String.Empty;

    [Range(0.01, Double.MaxValue)]
    public Decimal Price { get; set; }
}