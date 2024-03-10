using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ScanAndGoApi.Models
{
    [Table("USER")]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        public long id { get; set; }

        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

  
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }

        [JsonIgnore]
        public List<ShoppingList> ShoppingLists { get; set; }
    }
}
