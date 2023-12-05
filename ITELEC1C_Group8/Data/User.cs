using ITELEC1C_Group8.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}
