using AssetManagement.Api.Validation;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Api.DTOs
{
    public class AssetDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? SerialNumber { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;
    }

    public class SaveAssetDto
    {
        [Required]
        [NotWhiteSpace]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string? SerialNumber { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
