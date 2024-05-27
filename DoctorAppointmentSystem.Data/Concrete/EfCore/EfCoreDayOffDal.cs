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
    public class EfCoreDayOffDal : EfCoreGenericRepository<DayOff, Context>, IDayOffDal
    {
        public List<DayOff> GetDaysByDoctorId(int id)
        {
            using(var context = new Context()) 
            {
                return context.DaysOff
                    .Where(i=> i.DoctorId == id)
                    .ToList();
            }
        }

        public List<DayOff> GetAllByDoctor()
        {
            using( var context = new Context())
            {
                return context.DaysOff
                    .Include(i => i.doctor)
                    .Where(i=>i.StartDate >= DateTime.Now)
                    .ToList();
            }
        }
    }
}
