using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace Sprout.Exam.Core.EmployeeAggregate
{
    public class ContractualEmployee : Employee
    {
        public ContractualEmployee() 
        {}

        public override decimal CalculateSalary(decimal days, SalaryOption salaryOption) => throw new NotImplementedException();
    }
}
