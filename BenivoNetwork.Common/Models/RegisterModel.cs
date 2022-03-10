using System.ComponentModel.DataAnnotations;

namespace BenivoNetwork.Common.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

        public WelcomeModel ParentModel { get; set; }
    }
}
