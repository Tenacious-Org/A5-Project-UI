using A5.Models;
using Microsoft.EntityFrameworkCore;

namespace A5.Data
{
    public class AppDbContext:DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasOne<Employee>(r => r.ReportingPerson)
            .WithMany(e => e.Reportingpersons).HasForeignKey(r => r.ReportingPersonId);

            modelBuilder.Entity<Employee>().HasOne<Employee>(h => h.HR)
            .WithMany(e => e.Hrs).HasForeignKey(h => h.HRId);
        }

        public DbSet<AwardType> AwardTypes{ get; set; }
        public DbSet<Employee> Employees {get;set;}
        public DbSet<Award> Awards {get; set;}
        public DbSet<Comment> Comments {get; set;}
        public DbSet<Status> Statuses {get; set;}
        public DbSet<Organisation> Organisations{ get; set; }
        public DbSet<Department> Departments{ get; set; }
        public DbSet<Designation> Designations{ get; set; }
        
    }
}