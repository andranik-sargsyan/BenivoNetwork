using System.ComponentModel.DataAnnotations;

namespace BenivoNetwork.Common.Models
{
    public class RegisterModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }
        [Required]
        //TODO: use regex later
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

        public WelcomeModel ParentModel { get; set; }
    }
}
