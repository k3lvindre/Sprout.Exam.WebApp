using Sprout.Exam.Core.EmployeeAggregate;
using System.Threading.Tasks;

namespace Sprout.Exam.Application
{
    public interface IEmployeeRepository
    {
        Task<int> CreateEmployee(Employee employee);
    }
}
