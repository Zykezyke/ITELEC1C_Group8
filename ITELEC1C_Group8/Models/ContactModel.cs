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


        public string? CEmail { get; set; }


        public string? CFullName { get; set; }

        public string? CPhone { get; set; }
        public string? UserId { get; set; }

        public void SetUserInfo(UserManager<User> userManager, ClaimsPrincipal user)
        {
            var currentUser = userManager.GetUserAsync(user).Result;
            CEmail = currentUser?.Email;
            CFullName = currentUser?.FirstName + " " + currentUser?.LastName;
            CPhone = currentUser?.PhoneNumber;

            // Set the UserId property
            UserId = currentUser?.Id;
        }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Please Enter A Message")]
        public string Message { get; set; }
    }
}
