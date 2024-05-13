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
    public class EfCoreScheduleDal : EfCoreGenericRepository<Schedule,Context>, IScheduleDal
    {
        public void DeleteSchedulesById(int id)
        {
            using(var context = new Context()) 
            {
                var schedulesToRemove = context.Schedules.Where(i => i.DoctorId == id).ToList();
                context.Schedules.RemoveRange(schedulesToRemove);
                context.SaveChanges();
            }
        }

        public List<Schedule> GetSchedulesByDoctorId(int id)
        {
            using (var context = new Context())
            {
                return context.Schedules
                    .Where(i => i.DoctorId == id && i.IsWorkingDay == true)
                    .ToList();
            }
        }
    }
}
