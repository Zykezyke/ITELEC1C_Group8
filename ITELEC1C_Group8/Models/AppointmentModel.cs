using ITELEC1C_Group8.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using ITELEC1C_Group8.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITELEC1C_Group8.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }


        public string? AUserName { get; set; }


        public string? AFullName { get; set; }


        public string? APhone { get; set; }

        public void SetUserInfo(UserManager<User> userManager, ClaimsPrincipal user)
        {
            var currentUser = userManager.GetUserAsync(user).Result;
            AUserName = currentUser?.UserName;
            AFullName = currentUser?.FirstName + " " + currentUser?.LastName;
            APhone = currentUser?.PhoneNumber;
        }

        [Display(Name = "Branch")]
        public Branch SelectedBranch { get; set; }

        [Display(Name = "Doctor")]
        public string SelectedDoctor { get; set; }

        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please Set A Date")]
        public DateTime? AppDate { get; set; }
        [Display(Name = "Age")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        [Required(ErrorMessage = "Enter Your Age")]
        public int Age { get; set; }
        [Display(Name = "Notes")]
        [Required(ErrorMessage = "Please Enter A Note")]
        public string? Notes { get; set; }

        [Display(Name = "Status")]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        [Display(Name = "Doctor Notes")]
        public string? DoctorNotes { get; set; }
    }
}
