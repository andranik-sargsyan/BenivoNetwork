using System.ComponentModel.DataAnnotations;

namespace BenivoNetwork.Common.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email or User Name")]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
