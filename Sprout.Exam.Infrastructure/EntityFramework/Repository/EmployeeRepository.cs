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

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            _sproutExamContext.Employee.Update(employee);
            return await _sproutExamContext.SaveChangesAsync() > 0;
        }
        
        public async Task<bool> DeleteEmployeeByIdAsync(int id)
        {
            var employee = await this.GetEmployeeByIdAsync(id);
            _sproutExamContext.Employee.Remove(employee);
            return await _sproutExamContext.SaveChangesAsync() > 0;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id) => await _sproutExamContext.Employee.FindAsync(id);

        public async Task<IEnumerable<Employee>> GetEmployeesAsync() => await _sproutExamContext.Employee.ToListAsync();
    }
}
