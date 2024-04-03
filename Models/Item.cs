using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ScanAndGoApi.Models
{
    [Table("ITEM")]
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        public long Id { get; set; }

        [Required]
        public Product Product { get; set; }
        public Shelf Shelf { get; set; }
    }
}
