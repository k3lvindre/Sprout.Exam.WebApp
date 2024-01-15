using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Core.EmployeeAggregate;
using Sprout.Exam.Core.EmployeeAggregate.Enums;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;

namespace Sprout.Exam.Infrastructure
{
    public class SproutExamContext : DbContext
    {
        public SproutExamContext(DbContextOptions<SproutExamContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .HasDiscriminator<int>(x=>x.EmployeeTypeId)
                .HasValue<RegularEmployee>((int)EmployeeType.Regular)
                .HasValue<ContractualEmployee>((int)EmployeeType.Contractual);
        }

        public virtual DbSet<Employee> Employee { get; set; }
    }
}
