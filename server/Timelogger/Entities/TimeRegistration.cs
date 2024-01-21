using System;

namespace Timelogger.Entities
{
    public class TimeRegistration : BaseEntity
    {
        public int Minutes { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public Invoice? Invoice { get; set; }
        public int? InvoiceId { get; set; }
    }
}
