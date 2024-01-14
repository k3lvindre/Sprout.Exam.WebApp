using Sprout.Exam.Application;
using Sprout.Exam.Core.EmployeeAggregate;
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

        public async Task<int> CreateEmployee(Employee employee)
        {
            await _sproutExamContext.Employees.AddAsync(employee);
            return await _sproutExamContext.SaveChangesAsync();
        }
    }
}
