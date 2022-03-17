using BenivoNetwork.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BenivoNetwork.Common.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(64)]
        [RegularExpression("[a-zA-Z][a-zA-Z0-9_@.+-]{2,}")]
        public string UserName { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string LastName { get; set; }
        public RoleEnum Role { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderEnum? Gender { get; set; }
        public bool? IsMarried { get; set; }
        public string ImageURL { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public string RouteID => UserName == Email || UserName.Contains("@.+-") ? ID.ToString() : UserName;
        //TODO: fix with config
        public string ActualImageURL => string.IsNullOrWhiteSpace(ImageURL) ? "/Content/Images/user.png" : ImageURL;
    }
}
