using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timelogger.Entities;

namespace Timelogger.efConfiguration
{
    internal class TimeRegistrationConfiguration : IEntityTypeConfiguration<TimeRegistration>
    {
        public void Configure(EntityTypeBuilder<TimeRegistration> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .IsRequired();

            builder.HasOne(m => m.Project)
                .WithMany(m => m.TimeRegistrations)
                .HasForeignKey(m => m.ProjectId);
        }
    }
}
