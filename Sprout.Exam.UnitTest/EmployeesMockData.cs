using Moq;
using Sprout.Exam.Application;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Core.EmployeeAggregate;
using System.Collections.Generic;
using static Sprout.Exam.WebApp.Controllers.EmployeesController;

namespace Sprout.Exam.UnitTest
{
    public static class EmployeesMockData
    {
        public static readonly List<Employee> Employees = new List<Employee>()
        {
            new RegularEmployee
            {
                Birthdate = new System.DateTime(1993,03,25),
                FullName = "Jane Doe",
                Id = 1,
                Tin = "123215413",
                EmployeeTypeId = 1
            },
            new ContractualEmployee
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
