using ITELEC1C_Group8.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ITELEC1C_Group8.Data
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Doctor : User
    {
        public Branch Branch { get; set; }
        [NotMapped]
        public IFormFile? DoctorPfp { get; set; }

        [Display(Name = "Profile Picture")]
        public string? imagePath { get; set; }
    }
}
