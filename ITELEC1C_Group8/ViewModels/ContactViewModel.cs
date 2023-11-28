using ITELEC1C_Group8.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITELEC1C_Group8.ViewModels
{
    public class ContactViewModel : Contact
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public Contact NewContact { get; set; }
    }
}
