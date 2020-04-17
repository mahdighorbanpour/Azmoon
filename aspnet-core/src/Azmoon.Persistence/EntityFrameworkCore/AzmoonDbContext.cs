using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Azmoon.Authorization.Roles;
using Azmoon.Authorization.Users;
using Azmoon.MultiTenancy;
using Azmoon.Core.Quiz.Entities;

namespace Azmoon.EntityFrameworkCore
{
    public class AzmoonDbContext : AbpZeroDbContext<Tenant, Role, User, AzmoonDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Choice>  Choices { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public AzmoonDbContext(DbContextOptions<AzmoonDbContext> options)
            : base(options)
        {
        }
    }
}
