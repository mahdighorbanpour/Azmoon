using Azmoon.Core.Quiz.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azmoon.Persistence.EntityFrameworkCore.Configurations
{
    public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.Property(p => p.Marks)
                .IsRequired();

            builder.Property(p => p.QuestionId)
                .IsRequired();

            builder.Property(p => p.QuizId)
                .IsRequired();
        }
    }
}
