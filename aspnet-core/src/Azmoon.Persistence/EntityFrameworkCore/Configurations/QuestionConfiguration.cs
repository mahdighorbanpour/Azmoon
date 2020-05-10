using Azmoon.Core.Quiz.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azmoon.Persistence.EntityFrameworkCore.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(p => p.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(10000);

            builder.Property(p => p.Hint)
                .HasMaxLength(1000);

            builder.Property(p => p.Marks)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(p => p.QuestionType)
                .IsRequired();

            builder.Property(p => p.CategoryId)
               .IsRequired();
        }
    }
}
