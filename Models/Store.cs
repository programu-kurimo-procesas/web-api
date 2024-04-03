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
        public long Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }


        [JsonIgnore]
        public string MapUrl { get; set; }


        [JsonIgnore]
        public List<Shelf> Shelves { get; set; }
    }
}
