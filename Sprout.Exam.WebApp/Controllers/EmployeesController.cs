﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Core.EmployeeAggregate.Enums;
using Sprout.Exam.Application;
using Sprout.Exam.Infrastructure.EntityFramework.Repository;
using Sprout.Exam.Core.EmployeeAggregate;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeRepository.GetEmployeesAsync();
            var employeeList = _mapper.Map<IEnumerable<EmployeeDto>>(result);
            return Ok(employeeList);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == id));
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            var item = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == input.Id));
            if (item == null) return NotFound();
            item.FullName = input.FullName;
            item.Tin = input.Tin;
            item.Birthdate = input.Birthdate.ToString("yyyy-MM-dd");
            item.TypeId = input.TypeId;
            return Ok(item);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            Employee employee = default;

            if(input.TypeId == (int)EmployeeType.Regular)
            {
                employee = new RegularEmployee()
                {
                    Birthdate = input.Birthdate,
                    FullName = input.FullName,
                    Tin = input.Tin,
                    EmployeeTypeId = input.TypeId
                };
            }

            var result = await _employeeRepository.CreateEmployeeAsync(employee);

            return Created($"/api/employees/{result.Id}", result.Id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == id));
            if (result == null) return NotFound();
            StaticEmployees.ResultList.RemoveAll(m => m.Id == id);
            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id,decimal absentDays,decimal workedDays)
        {
            var result = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == id));

            if (result == null) return NotFound();
            var type = (EmployeeType) result.TypeId;
            return type switch
            {
                EmployeeType.Regular =>
                    //create computation for regular.
                    Ok(25000),
                EmployeeType.Contractual =>
                    //create computation for contractual.
                    Ok(20000),
                _ => NotFound("Employee Type not found")
            };

        }

    }
}
