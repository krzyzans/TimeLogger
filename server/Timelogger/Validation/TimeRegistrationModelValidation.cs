using FluentValidation;
using System;
using Timelogger.Models;

namespace Timelogger.Validation
{
    public class TimeRegistrationModelValidation : AbstractValidator<TimeRegistrationModel>
    {
        public TimeRegistrationModelValidation()
        {
            RuleFor(p => p.Minutes).GreaterThanOrEqualTo(30);
            RuleFor(p => p.InvoiceId).NotEmpty().NotNull();
        }
    }
}
