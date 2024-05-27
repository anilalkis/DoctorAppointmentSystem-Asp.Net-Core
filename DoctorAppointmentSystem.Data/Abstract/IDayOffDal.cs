using DoctorAppointmentSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Data.Abstract
{
    public interface IDayOffDal : IRepository<DayOff>
    {
        List<DayOff> GetDaysByDoctorId(int id);
        List<DayOff> GetAllByDoctor();
    }
}
