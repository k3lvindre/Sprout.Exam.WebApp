using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Core.EmployeeAggregate;

namespace Sprout.Exam.Infrastructure
{
    public class SproutExamContext : DbContext
    {
        public SproutExamContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }
}
