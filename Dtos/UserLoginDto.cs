using System.ComponentModel.DataAnnotations;

namespace ScanAndGoApi.Dtos
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
