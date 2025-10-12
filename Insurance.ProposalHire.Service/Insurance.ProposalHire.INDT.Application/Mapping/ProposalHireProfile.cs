using AutoMapper;
using INDT.Common.Insurance.Dto.Response;
using ProposalHireEntity = INDT.Common.Insurance.Domain.ProposalHire;

namespace Insurance.ProposalHire.INDT.Application.Mapping
{
    public class ProposalHireProfile: Profile
    {
        public ProposalHireProfile()
        {
            CreateMap<ProposalHireDto, ProposalHireEntity>().ReverseMap();
        }
        

    }
}
