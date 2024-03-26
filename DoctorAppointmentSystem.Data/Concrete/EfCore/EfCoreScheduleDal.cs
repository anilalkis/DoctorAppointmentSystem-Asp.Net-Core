using DoctorAppointmentSystem.Data.Abstract;
using DoctorAppointmentSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Data.Concrete.EfCore
{
    public class EfCoreScheduleDal : EfCoreGenericRepository<Schedule,Context>, IRepository<Schedule>
    {

    }
}
