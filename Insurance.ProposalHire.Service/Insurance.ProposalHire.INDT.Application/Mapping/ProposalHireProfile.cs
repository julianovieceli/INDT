using AutoMapper;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Response;
using ProposalHireEntity = INDT.Common.Insurance.Domain.ProposalHire;

namespace Insurance.ProposalHire.INDT.Application.Mapping
{
    public class ProposalHireProfile: Profile
    {
        public ProposalHireProfile()
        {
            CreateMap<ProposalHireDto, ProposalHireEntity>().ReverseMap();

            CreateMap<ProposalHireEntity, ProposalHireDto>()
            .ForMember(to => to.InsuranceName, opt => opt.MapFrom(from => from.Proposal.Insurance.Name))
            .ForMember(to => to.Value, opt => opt.MapFrom(from => from.Proposal.Value))
            .ForMember(to => to.ClientName, opt => opt.MapFrom(from => from.Proposal.Client.Name))
            .ForMember(to => to.InsuranceId, opt => opt.MapFrom(from => from.Proposal.Insurance.Id));
             
        }

        
        

    }

    public class ProposalProfile : Profile
    {
        public ProposalProfile()
        {
            CreateMap<ProposalDto, Proposal>();
        }

        
        

    }
}
