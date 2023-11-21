using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITELEC1C_Group8.Models;

namespace ITELEC1C_Group8.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment()
                {
                    AppointmentId = 1,
                    UserName = "Test1",
                    Phone = "09179873357",
                    AppBranch = Branch.Branch2,
                    Doctor = Doctor.Doc2,
                    AppDate = DateTime.Parse("2022-08-07"),
                    Age = 23,
                    Notes = "hiiiii"
                },
                new Appointment()
                {
                    AppointmentId = 2,
                    UserName = "Test2",
                    Phone = "09179873357",
                    AppBranch = Branch.Branch3,
                    Doctor = Doctor.Doc3,
                    AppDate = DateTime.Parse("2022-02-07"),
                    Age = 22,
                    Notes = "hiiiiii"
                }

                );
        }
    }
}