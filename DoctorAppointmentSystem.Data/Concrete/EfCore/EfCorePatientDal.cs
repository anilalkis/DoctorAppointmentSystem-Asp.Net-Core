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
    public class EfCorePatientDal : EfCoreGenericRepository<Patient, Context>, IPatientDal
    {
        public Patient GetByUserEmail(string UserEmail)
        {
            using(var context = new Context()) 
            {
                return context.Patients.FirstOrDefault(i => i.Email == UserEmail);
            }
        }

        public Patient GetPatientWithAppointment(int id)
        {
            using (var context = new Context())
            {
                return context.Patients
                    .Include(i => i.Appointments)
                    .FirstOrDefault(i => i.PatientId == id);
            }
        }
    }
}
