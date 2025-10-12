using AutoMapper;
using INDT.Common.Insurance.Dto.Response;

namespace Insurance.Proposal.INDT.Application.Mapping
{
    public class InsuranceProfile : Profile
    {
        public InsuranceProfile()
        {
            CreateMap<InsuranceDto, Insurance.INDT.Domain.Insurance>().ReverseMap();
        }
    }
}
