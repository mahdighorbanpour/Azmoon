using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Azmoon.EntityFrameworkCore
{
    public static class AzmoonDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AzmoonDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AzmoonDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
