using DoctorAppointmentSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Data.Abstract
{
    public interface IDoctorDal : IRepository<Doctor>
    {
        Doctor GetDoctorWithSchedule(int id);
        Doctor GetDoctorWithAppointment(int id);
        Doctor GetByUserEmail(string UserEmail);
    }
}
