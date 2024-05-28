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

        public List<MonthlyPatientCount> GetMonthlyPatientCounts()
        {
            using (var context = new Context())
            {
                var sixMonthsAgo = DateTime.Now.AddMonths(-6);

                return context.Patients
                    .Where(a => a.Created >= sixMonthsAgo)
                    .GroupBy(a => new { Year = a.Created.Year, Month = a.Created.Month })
                    .Select(g => new MonthlyPatientCount
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        Count = g.Count()
                    })
                    .OrderBy(result => result.Year)
                    .ThenBy(result => result.Month)
                    .ToList();
            }
        }

        public int GetPatientCountLastYear()
        {
            using (var context = new Context())
            {
                var lastYear = DateTime.Now.AddYears(-1).Year;

                return context.Patients
                    .Count(a => a.Created.Year == lastYear);
            }
        }

        public int GetPatientCountThisYear()
        {
            using (var context = new Context())
            {
                var thisYear = DateTime.Now.Year;

                return context.Patients
                    .Count(a => a.Created.Year == thisYear);
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
