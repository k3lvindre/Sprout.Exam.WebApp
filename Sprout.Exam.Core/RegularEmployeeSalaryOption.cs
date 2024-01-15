using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Core
{
    public class RegularEmployeeSalaryOption : SalaryOption
    {
        public decimal Tax { get; set; }
        public decimal Dividend { get; set; }
    }
}
