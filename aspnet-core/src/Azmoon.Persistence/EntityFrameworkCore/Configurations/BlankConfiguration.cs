using Azmoon.Core.Quiz.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azmoon.Persistence.EntityFrameworkCore.Configurations
{
    public class BlankConfiguration : IEntityTypeConfiguration<Blank>
    {
        public void Configure(EntityTypeBuilder<Blank> builder)
        {
            builder.Property(p => p.Answer)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Index)
                .IsRequired();

        }
    }
}
