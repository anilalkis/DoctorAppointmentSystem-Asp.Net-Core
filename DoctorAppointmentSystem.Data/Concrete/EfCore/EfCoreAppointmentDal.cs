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
    public class EfCoreAppointmentDal : EfCoreGenericRepository<Appointment, Context>, IAppointmentDal
    {
        public List<Appointment> GetAppointmentsWithDoctorId(int id)
        {
            using (var context = new Context())
            {
                return context.Appointments
                    .Where(i => i.DoctorId == id)
                    .ToList();
            }
        }

        public List<Appointment> GetAppointmentsWithPatientId(int id)
        {
            using (var context = new Context())
            {
                return context.Appointments
                    .Where(i => i.PatientId == id)
                    .Include(i => i.Doctor)
                    .ToList();
            }

        }
    }

}
