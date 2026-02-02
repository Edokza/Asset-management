using AssetManagement.Api.Validation;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Api.Models
{
    public class Asset
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? SerialNumber { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}

