using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sprout.Exam.Application;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Core;
using Sprout.Exam.Core.EmployeeAggregate;
using Sprout.Exam.Core.EmployeeAggregate.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public RegularEmployeeSalaryOption _regularEmployeeSalaryOption { get; set; }

        public EmployeesController(IEmployeeRepository employeeRepository
            , IMapper mapper
            , IOptions<RegularEmployeeSalaryOption> regularEmployeeSalaryOption)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _regularEmployeeSalaryOption = regularEmployeeSalaryOption?.Value;
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
            var result = await _employeeRepository.GetEmployeeByIdAsync(id);
            var employee = _mapper.Map<EmployeeDto>(result);

            return Ok(employee);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            var item = await _employeeRepository.GetEmployeeByIdAsync(input.Id);
            if (item == null) return NotFound();
            item.FullName = input.FullName;
            item.Tin = input.Tin;
            item.Birthdate = input.Birthdate;
            item.EmployeeTypeId = input.TypeId;
            return await _employeeRepository.UpdateEmployeeAsync(item) ? Accepted() : BadRequest();
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
            var result = await _employeeRepository.DeleteEmployeeByIdAsync(id);

            if (!result) return BadRequest();
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
        public async Task<IActionResult> Calculate(CalculateRequest calculateModel)
        {
            var result = await _employeeRepository.GetEmployeeByIdAsync(calculateModel.id);
            if (result == null) return NotFound();

            var type = (EmployeeType) result.EmployeeTypeId;
            switch (type)
            {
                case EmployeeType.Regular:
                    //Kelvin: Yes we can use factory pattern for salary here.
                    var employee = result as RegularEmployee;
                    return Ok(employee.CalculateSalary(calculateModel.absentDays, _regularEmployeeSalaryOption));
                case EmployeeType.Contractual:
                    break;
            }

            return BadRequest();
        }

        public class CalculateRequest
        {
            public int id { get; set; }
            public decimal absentDays { get; set; }
            public decimal workedDays { get; set; }
        }
    }
}
