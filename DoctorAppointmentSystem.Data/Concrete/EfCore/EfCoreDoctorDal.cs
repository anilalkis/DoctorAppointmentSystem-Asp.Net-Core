using DoctorAppointmentSystem.Data.Abstract;
using DoctorAppointmentSystem.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Data.Concrete.EfCore
{
    public class EfCoreDoctorDal : EfCoreGenericRepository<Doctor, Context>, IDoctorDal
    {
        public Doctor GetByUserName(string UserName)
        {
            using (var context = new Context())
            {
                return context.Doctors.FirstOrDefault(i => i.FullName == UserName);
            }
        }

        public Doctor GetDoctorWithAppointment(int id)
        {
            using (var context = new Context())
            {
                return context.Doctors
                    .Include(i => i.Appointments)
                    .FirstOrDefault(i => i.DoctorId == id);
            }
        }

        public Doctor GetDoctorWithSchedule(int id)
        {
            using(var context = new Context()) 
            {
                return context.Doctors
                    .Include(i => i.Schedules)
                    .Where(i => i.Schedules.Any(s => s.IsWorkingDay == true))
                    .FirstOrDefault(i => i.DoctorId == id);
            }
        }
    }
}
