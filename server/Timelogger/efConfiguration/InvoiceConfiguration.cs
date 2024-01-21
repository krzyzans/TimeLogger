using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timelogger.Entities;

namespace Timelogger.efConfiguration
{
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .IsRequired();

            builder.HasOne(m => m.Project)
                .WithMany(m => m.Invoices)
                .HasForeignKey(m => m.ProjectId);

            builder.HasMany(m => m.TimeRegistrations)
                .WithOne(m => m.Invoice)
                .HasForeignKey(m => m.InvoiceId);
        }
    }
}
