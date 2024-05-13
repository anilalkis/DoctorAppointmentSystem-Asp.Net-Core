using DoctorAppointmentSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Data.Abstract
{
    public interface IScheduleDal : IRepository<Schedule>
    {
        List<Schedule> GetSchedulesByDoctorId(int id);
        void DeleteSchedulesById(int id);
    }
}
