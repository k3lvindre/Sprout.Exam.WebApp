using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Core.EmployeeAggregate;

namespace Sprout.Exam.WebApp
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegularEmployee, EmployeeDto>()
                .ForMember(m => m.Birthdate, s => s.MapFrom(y => y.Birthdate.ToString("MM/dd/yyyy")))
                .ForMember(m => m.TypeId, s => s.MapFrom(y => y.EmployeeTypeId));
            CreateMap<ContractualEmployee, EmployeeDto>()
                .ForMember(m => m.Birthdate, s => s.MapFrom(y => y.Birthdate.ToString("MM/dd/yyyy")))
                .ForMember(m => m.TypeId, s => s.MapFrom(y => y.EmployeeTypeId));
        }
    }
}
