using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Application;
using Sprout.Exam.Core.EmployeeAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.Infrastructure.EntityFramework.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SproutExamContext _sproutExamContext;

        public EmployeeRepository(SproutExamContext sproutExamContext)
        {
            _sproutExamContext = sproutExamContext;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await _sproutExamContext.Employee.AddAsync(employee);
            await _sproutExamContext.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync() => await _sproutExamContext.Employee.ToListAsync();
    }
}
