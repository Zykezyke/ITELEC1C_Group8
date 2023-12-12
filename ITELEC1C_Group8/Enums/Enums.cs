using ITELEC1C_Group8.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ITELEC1C_Group8.Enums
{
    public enum Branch
    {
        Taguig, Manila, Pasig, Makati, Ortigas
    }

    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Declined
    }

}
