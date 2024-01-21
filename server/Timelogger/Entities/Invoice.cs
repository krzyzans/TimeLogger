using System.Collections.Generic;

namespace Timelogger.Entities
{
    public class Invoice : BaseEntity
    {
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public ICollection<TimeRegistration> TimeRegistrations { get; set; }
    }
}
