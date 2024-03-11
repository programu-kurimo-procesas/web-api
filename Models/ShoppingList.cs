using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ScanAndGoApi.Models
{
    [Table("SHOPPING_LIST")]
    public class ShoppingList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        public long Id { get; set; }

        public List<ProductListAsc>? ProductsInList{ get; set; }
        public User User { get; set; }

    }
}
