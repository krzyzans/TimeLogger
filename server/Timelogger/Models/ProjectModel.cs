using System;
using System.Linq;
using Timelogger.Entities;

namespace Timelogger.Models
{
    public class ProjectModel
    {
        public ProjectModel()
        {
            
        }

        public ProjectModel(Project project)
        {
            this.DeadLine = project.DeadlineTime;
            this.Name = project.Name;
            this.Ident = project.Id;
            this.ReservationSum = project.TimeRegistrations.Sum(tr => tr.Minutes);
        }

        public int Ident { get; set; }
        public DateTime DeadLine { get; set; }
        public int ReservationSum { get; set; }
        public string Name { get; set; }
    }
}