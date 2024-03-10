using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ScanAndGoApi.Models
{
    [Table("PRODUCT_LIST_ASC")]
    public class ProductListAsc
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        public long Id { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public ShoppingList ShoppingList { get; set; }
    }
}
