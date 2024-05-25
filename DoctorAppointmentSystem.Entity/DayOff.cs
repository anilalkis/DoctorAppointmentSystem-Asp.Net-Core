using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Entity
{
	public class DayOff
	{
        public int Id { get; set; }
		public int DoctorId { get; set; }
		public Doctor? doctor { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan? Interval { get; set; }
	}
}
