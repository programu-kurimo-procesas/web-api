using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ScanAndGoApi.Models
{
    [Table("STORE")]
    public class Store
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        public long Id { get; set; }
        public string Address { get; set; }

        public List<Item> Items { get; set; }
    }
}
