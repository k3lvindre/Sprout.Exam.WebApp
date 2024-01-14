using Sprout.Exam.Core.EmployeeAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.Application
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}
