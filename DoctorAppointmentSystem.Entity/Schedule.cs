using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Entity
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Interval { get; set; } // Yarım saatlik aralık
        public int DoctorId { get; set; }

        //public string WorkingHours { get; set; }
        public bool IsWorkingDay { get; set; }
        public Doctor Doctor { get; set; }
    }
}
