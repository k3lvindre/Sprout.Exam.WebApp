using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sprout.Exam.Application;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Core;
using Sprout.Exam.WebApp;
using Sprout.Exam.WebApp.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Sprout.Exam.WebApp.Controllers.EmployeesController;

namespace Sprout.Exam.UnitTest.Controller
{

    [TestClass]
    public class EmployeesController_UnitTest
    {
        [TestMethod]
        [DynamicData(nameof(CalculateRequestData))]
        public async Task EmployeesController_CalculateOk(CalculateRequest calculateRequest, decimal expected)
        {
            // Arrange
            var controller = SetupEmployeesController(calculateRequest);

            // Act
            var result = await controller.Calculate(calculateRequest);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expected, okResult.Value);
        }

        [TestMethod]
        [DataRow(1, "Jane Doe")]
        [DataRow(2, "John Doe")]
        public async Task EmployeesController_GetByIdOk(int id, string expectedName)
        {
            // Arrange
            var controller = SetupEmployeesController(
                new CalculateRequest() { id = id });
            
            // Act
            var result = await controller.GetById(id);
            var okResult = result as OkObjectResult;
            var employee = okResult?.Value as EmployeeDto;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expectedName, employee.FullName);
        }

        public static IEnumerable<object[]> CalculateRequestData
        {
            get
            {
                return new[]
                {
                    new object[] { new CalculateRequest() { absentDays = 1, id = 1, workedDays = 0 }, 16690.91m },
                    new object[] { new CalculateRequest() { absentDays = 2, id = 1, workedDays = 0 }, 15781.82m },
                };
            }
        }

        private EmployeesController SetupEmployeesController(CalculateRequest calculateRequest)
        {
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(repo => repo.GetEmployeeByIdAsync(calculateRequest.id))
                .ReturnsAsync(EmployeesMockData.Employees.Where(x => x.Id == calculateRequest.id).FirstOrDefault());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            IOptions<RegularEmployeeSalaryOption> regularEmployeeSalaryOption = Options.Create(new RegularEmployeeSalaryOption()
            {
                Dividend = 22,
                Salary = 20_000,
                Tax = 0.12m
            });

            var controller = new EmployeesController(mockRepo.Object
                , mapper
                , regularEmployeeSalaryOption);

            return controller;
        }
    }


}
