using Azmoon.Core.Quiz.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azmoon.Persistence.EntityFrameworkCore.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.ShortDescription)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(p => p.LongDescription)
               .HasMaxLength(1000)
               .IsRequired();

            builder.Property(p => p.ImageUri)
               .HasMaxLength(250);

            builder.HasMany<Quiz>(c => c.Quizzes)
                .WithOne(q => q.Category)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
