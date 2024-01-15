using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Core.EmployeeAggregate;
using System.Collections.Generic;

namespace Sprout.Exam.UnitTest
{
    public static class StaticEmployees
    {
        public static readonly List<Employee> employeeDtos = new List<Employee>()
        {
            new RegularEmployee
            {
                Birthdate = new System.DateTime(1993,03,25),
                FullName = "Jane Doe",
                Id = 1,
                Tin = "123215413",
                EmployeeTypeId = 1
            },
            new RegularEmployee
            {
                Birthdate = new System.DateTime(1993,05,28),
                FullName = "John Doe",
                Id = 2,
                Tin = "957125412",
                EmployeeTypeId = 2
            }
        };
    }
}
