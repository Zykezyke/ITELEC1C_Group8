using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ITELEC1C_Group8.ViewModels
{
    public class UpdateViewModel
    {
        [HiddenInput]
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "A user name is required")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression("[0]{1}[0-9]{3}[0-9]{3}[0-9]{4}")]
        [Required(ErrorMessage = "A phone number is required")]
        public string Phone { get; set; }
    }
}
