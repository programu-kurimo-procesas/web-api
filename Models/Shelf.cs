using ScanAndGoApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScanAndGoApi.Models
{
    [Table("SHELF")]
    public class Shelf
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        public string X1 { get; set; }
        public string Y1 { get; set; }
        public string X2 { get; set; }
        public string Y2 { get; set; }

        [JsonIgnore]
        public Store Store { get; set; }

        [JsonIgnore]
        public List<Item> Items { get; set; }
    }
}
