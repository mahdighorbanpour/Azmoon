using Azmoon.Core.Quiz.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azmoon.Persistence.EntityFrameworkCore.Configurations
{
    public class MatchSetConfiguration : IEntityTypeConfiguration<MatchSet>
    {
        public void Configure(EntityTypeBuilder<MatchSet> builder)
        {
            builder.Property(p => p.Value)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
