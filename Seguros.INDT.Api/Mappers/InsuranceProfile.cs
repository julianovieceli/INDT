using AutoMapper;
using Insurance.INDT.Dto.Response;

namespace Insurance.Proposal.INDT.Api.Mappers
{
    public class InsuranceProfile : Profile
    {
        public InsuranceProfile()
        {
            CreateMap<InsuranceDto, Insurance.INDT.Domain.Insurance>().ReverseMap();
        }
    }
}
