using System.ComponentModel.DataAnnotations;

namespace DP_Shop.DTOs.Products
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [FutureDate(ErrorMessage = "Expiry date must be in the future")]
        public DateTime? ExpiryDate { get; set; }

        [Required(ErrorMessage = "Category ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be greater than 0")]
        public int CategoryId { get; set; }
    }
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime expiryDate)
            {
                if (expiryDate <= DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Expiry date must be in the future");
                }
            }
            return ValidationResult.Success;
        }
    }
}
