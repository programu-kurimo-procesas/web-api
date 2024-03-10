using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ScanAndGoApi.Models
{
    [Table("PRODUCT")]
    public class Product
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        public string? Image { get; set; }

        [JsonIgnore]
        public List<Item>? Items { get; set; }

        [JsonIgnore]
        public List<ProductListAsc>? ProductListAsc { get; set; }

    }
}
