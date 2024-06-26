﻿using System;
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
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public TimeSpan? Interval { get; set; }
		public State IsApproved { get; set; }
	}

	public enum State
	{
		Waiting = 0,
		NotApproved = 1,
		Approved = 2
	}
}
