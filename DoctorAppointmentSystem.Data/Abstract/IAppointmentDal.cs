using DoctorAppointmentSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Data.Abstract
{
    public interface IAppointmentDal : IRepository<Appointment>
    {
        List<Appointment> GetAppointmentsWithDoctorId(int id);
        List<Appointment> GetAppointmentsWithPatientId(int id);


    }
}
