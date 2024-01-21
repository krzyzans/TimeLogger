using FluentValidation;
using System;
using Timelogger.Models;

namespace Timelogger.Validation
{
    public class ProjectModelValidation : AbstractValidator<ProjectModel>
    {
        public ProjectModelValidation()
        {
            RuleFor(p => p.DeadLine).GreaterThan(DateTime.Today.ToUniversalTime());
            RuleFor(p => p.Name).NotEmpty().NotNull();
        }
    }
}
