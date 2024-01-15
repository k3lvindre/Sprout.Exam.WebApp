using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sprout.Exam.Application;
using Sprout.Exam.Core;
using Sprout.Exam.WebApp.Controllers;
using Sprout.Exam.WebApp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static Sprout.Exam.WebApp.Controllers.EmployeesController;

namespace Sprout.Exam.UnitTest
{

    [TestClass]
    public class EmployeeController_UnitTest
    {
        [TestMethod]
        [DynamicData(nameof(CalculateRequestData))]
        public async Task EmployeeController_CalculateOk(CalculateRequest calculateRequest, decimal expected)
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(repo => repo.GetEmployeesAsync())
                .ReturnsAsync(StaticEmployees.employeeDtos); 
            mockRepo.Setup(repo => repo.GetEmployeeByIdAsync(calculateRequest.id))
                .ReturnsAsync(StaticEmployees.employeeDtos.Where(x=>x.Id == calculateRequest.id).FirstOrDefault());

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile()); 
            });
            var mapper = mockMapper.CreateMapper();

            IOptions<RegularEmployeeSalaryOption> regularEmployeeSalaryOption = Options.Create<RegularEmployeeSalaryOption>(new RegularEmployeeSalaryOption()
            {
                Dividend = 22,
                Salary = 20_000,
                Tax = 0.12m
            });

            var controller = new EmployeesController(mockRepo.Object, mapper, regularEmployeeSalaryOption);
           

            // Act
            var result = await controller.Calculate(calculateRequest);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expected, okResult.Value);
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
    }

   
}
