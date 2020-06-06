using Azmoon.Core.Quiz.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azmoon.Persistence.EntityFrameworkCore.Configurations
{
    public class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
    {
        public void Configure(EntityTypeBuilder<Choice> builder)
        {
            builder.Property(p => p.Value)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.IsCorrect)
                .IsRequired();

            builder.Property(p => p.QuestionId)
               .IsRequired();

            builder.HasOne(p => p.MatchSet)
                .WithMany(p => p.Choices)
                .HasForeignKey(p => p.MatchSetId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
