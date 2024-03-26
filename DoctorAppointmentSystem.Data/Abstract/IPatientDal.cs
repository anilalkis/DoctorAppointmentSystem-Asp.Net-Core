using DoctorAppointmentSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Data.Abstract
{
    public interface IPatientDal : IRepository<Patient>
    {
        Patient GetPatientWithAppointment(int id);
    }
}
