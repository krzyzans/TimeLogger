using System;
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
        }

        public int Ident { get; set; }
        public DateTime DeadLine { get; set; }
        public string Name { get; set; }
    }
}
