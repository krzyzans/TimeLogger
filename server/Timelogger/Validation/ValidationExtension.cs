using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Timelogger.Validation
{
    public static class ValidationExtension
    {
        public static void AddValidations(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ProjectModelValidation>();
        }
    }
}
