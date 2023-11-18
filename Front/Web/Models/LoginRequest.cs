using System.ComponentModel.DataAnnotations;

namespace Mardev.Arq.Front.Web.Models
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
