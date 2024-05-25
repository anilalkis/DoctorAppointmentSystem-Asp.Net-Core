using DoctorAppointmentSystem.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Data.Concrete.EfCore
{
    public class Context : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=ANIL\SQLEXPRESS;Database=AppointmentSystemDB;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DayOff> DaysOff { get; set; }

	}
}
