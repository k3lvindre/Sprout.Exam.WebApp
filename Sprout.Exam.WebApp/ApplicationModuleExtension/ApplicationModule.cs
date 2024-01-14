using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.Application;
using Sprout.Exam.Infrastructure;
using Sprout.Exam.Infrastructure.EntityFramework.Repository;

namespace Sprout.Exam.WebApp.ApplicationModuleExtension
{
    public static class Extensions
    {

        public static void AddSproutDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SproutExamContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
