using DoctorAppointmentSystem.Data.Abstract;
using DoctorAppointmentSystem.Entity;
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
    }
}
