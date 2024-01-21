using Timelogger.Entities;

namespace Timelogger.Models
{
    public class TimeRegistrationModel
    {
        public TimeRegistrationModel()
        {
            
        }

        public TimeRegistrationModel(TimeRegistration timeRegistration)
        {
            this.Minutes = timeRegistration.Minutes;
            this.InvoiceId = timeRegistration.InvoiceId;
            this.ProjectId = timeRegistration.ProjectId;
        }

        public int Minutes { get; set; }
        public int ProjectId { get; set; }
        public int? InvoiceId { get; set; }
    }
}
