using AssetManagement.Api.Validation;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Api.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }

    public class SaveCategoryDto
    {
        [Required]
        [NotWhiteSpace]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
