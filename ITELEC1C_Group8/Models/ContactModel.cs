using ITELEC1C_Group8.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ITELEC1C_Group8.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public string? Email { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public string? FullName { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public string? Phone { get; set; }

        public void SetUserInfo(UserManager<User> userManager, ClaimsPrincipal user)
        {
            var currentUser = userManager.GetUserAsync(user).Result;
            Email = currentUser?.Email;
            FullName = currentUser?.FirstName + currentUser?.LastName;
            Phone = currentUser?.PhoneNumber;
        }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Please Enter A Message")]
        public string Message { get; set; }
    }
}
