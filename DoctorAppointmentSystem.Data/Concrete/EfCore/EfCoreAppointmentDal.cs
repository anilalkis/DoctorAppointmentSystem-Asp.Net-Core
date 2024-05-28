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
        public Appointment GetAppointmentById(int id)
        {
            using (var context = new Context())
            {
                return context.Appointments
                    .Where(i => i.AppointmentId == id)
                    .Include(i => i.Patient)
                    .Include(i => i.Doctor)
                    .FirstOrDefault();
            }
        }

        public int GetAppointmentsCountThisYear()
        {
            using (var context = new Context())
            {
                var currentYear = DateTime.Now.Year;

                return context.Appointments
                    .Count(a => a.DateTime.Year == currentYear);
            }
        }

        public int GetAppointmentsCountLastYear()
        {
            using (var context = new Context())
            {
                var lastYear = DateTime.Now.AddYears(-1).Year;

                return context.Appointments
                    .Count(a => a.DateTime.Year == lastYear);
            }
        }


        public List<Appointment> GetAppointmentsWithDoctorId(int id)
        {
            using (var context = new Context())
            {
                return context.Appointments
                    .Where(i => i.DoctorId == id)
                    .Include(i => i.Patient)
                    .Include(i => i.Doctor)
                    .OrderBy(i => i.DateTime)
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
                    .OrderBy(i => i.DateTime)
                    .ToList();
            }

        }

        public List<MonthlyAppointmentCount> GetMonthlyAppointmentCounts()
        {
            using (var context = new Context())
            {
                var sixMonthsAgo = DateTime.Now.AddMonths(-6);

                return context.Appointments
                    .Where(a => a.DateTime >= sixMonthsAgo)
                    .GroupBy(a => new { Year = a.DateTime.Year, Month = a.DateTime.Month })
                    .Select(g => new MonthlyAppointmentCount
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
    }

}
