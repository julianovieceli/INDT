using AutoMapper;
using INDT.Common.Insurance.Dto.Response;
using InsuranceDomain = INDT.Common.Insurance.Domain;

namespace Insurance.INDT.Application.Mapping
{
    public class InsuranceProfile : Profile
    {
        public InsuranceProfile()
        {
            CreateMap<InsuranceDto, InsuranceDomain.Insurance>().ReverseMap();
        }
    }
}
