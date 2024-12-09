using Microsoft.EntityFrameworkCore;
using SRC_Travel_Agencies.Models;

namespace SRC_Travel_Agencies.Context
{
    public class SqlContext:DbContext      
    {
        public SqlContext(DbContextOptions<SqlContext> option) : base(option)
        {

        }
        public DbSet<Upload_Bus> uploads { get; set; }
        public DbSet<Bus_Category> Category { get; set; }
        public DbSet<Employees> Employees { get; set; } = default!;
        public DbSet<reserve1>  Reserves { get; set; } = default!;
        public DbSet<reserve2>  Reserves2 { get; set; } = default!;
        public DbSet<reserve3>  Reserves3 { get; set; } = default!;
        public DbSet<reserve4>  Reserves4 { get; set; } = default!;
        public DbSet<SRC_Travel_Agencies.Models.Ticket> Ticket { get; set; } = default!;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employees>()
        //        .HasIndex(p => new { p.Email })
        //        .IsUnique(true);

        //    modelBuilder.Entity<Employees>()
        //        .HasIndex(p => new { p.PhoneNumber })
        //        .IsUnique(true);
        //}

    }
}
