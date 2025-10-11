using AutoMapper;
using Insurance.INDT.Dto.Response;

namespace Insurance.Proposal.INDT.Api.Mappers
{
    public class ProposalProfile : Profile
    {
        public ProposalProfile()
        {
            CreateMap<ProposalDto, Insurance.INDT.Domain.Proposal>().ReverseMap();

            CreateMap<Insurance.INDT.Domain.Proposal, ProposalDto>()
              .ForMember(to => to.InsuranceName, opt => opt.MapFrom(from => from.Insurance.Name))
              .ForMember(to => to.InsuranceId, opt => opt.MapFrom(from => from.Insurance.Id))
              .ForMember(to => to.ClientName, opt => opt.MapFrom(from => from.Client.Name))
               .ForMember(to => to.Status, opt => 
               opt.MapFrom(from => (from.StatusId == Insurance.INDT.Domain.Enums.ProposalStatus.Approved)
               ? "Approved" : (from.StatusId == Insurance.INDT.Domain.Enums.ProposalStatus.Rejected) ? "Rejected" : "Analysing"));
               




        }
    }
}
