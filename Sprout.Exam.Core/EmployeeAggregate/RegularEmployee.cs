using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace Sprout.Exam.Core.EmployeeAggregate
{
    public class RegularEmployee : Employee
    {
        public RegularEmployee() 
        {}

        public override decimal CalculateSalary(decimal days, SalaryOption salaryOption) {
            var regularEmployeeSalaryOption = salaryOption as RegularEmployeeSalaryOption;
            if (regularEmployeeSalaryOption is null) return 0;

            var salary = regularEmployeeSalaryOption.Salary - GetSalaryDeduction() - GetTaxDeduction();
            return Math.Round(salary, 2);

            //days = number of days absent
            decimal GetSalaryDeduction() => (regularEmployeeSalaryOption.Salary / regularEmployeeSalaryOption.Dividend) * days;
            decimal GetTaxDeduction() => regularEmployeeSalaryOption.Salary * regularEmployeeSalaryOption.Tax;
        }
    }
}
