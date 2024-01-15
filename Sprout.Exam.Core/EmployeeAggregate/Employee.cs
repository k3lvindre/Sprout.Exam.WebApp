using System;

namespace Sprout.Exam.Core.EmployeeAggregate
{
    public abstract class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Tin { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; }

        public abstract decimal CalculateSalary(decimal days, SalaryOption salaryOption);
    }
}
