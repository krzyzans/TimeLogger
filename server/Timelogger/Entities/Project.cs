using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
	public class Project : BaseEntity
	{
		public string Name { get; set; }
        public DateTime DeadlineTime { get; set; }

        public ICollection<TimeRegistration> TimeRegistrations { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
