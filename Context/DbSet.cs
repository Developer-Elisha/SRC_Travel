using Microsoft.EntityFrameworkCore;

namespace SRC_Travel_Agencies.Context
{
    public class DbSet
    {
        private DbContextOptions<SqlContext> option;

        public DbSet(DbContextOptions<SqlContext> option)
        {
            this.option = option;
        }
    }
}