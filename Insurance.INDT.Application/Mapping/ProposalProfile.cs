using AutoMapper;
using INDT.Common.Insurance.Domain.Enums;
using INDT.Common.Insurance.Dto.Response;
using INDT.Common.Insurance.Domain;

namespace Insurance.INDT.Application.Mapping
{
    public class ProposalProfile : Profile
    {
        public ProposalProfile()
        {
            CreateMap<ProposalDto, Proposal>().ReverseMap();

            CreateMap<Proposal, ProposalDto>()
              .ForMember(to => to.InsuranceName, opt => opt.MapFrom(from => from.Insurance.Name))
              .ForMember(to => to.InsuranceId, opt => opt.MapFrom(from => from.Insurance.Id))
              .ForMember(to => to.ClientName, opt => opt.MapFrom(from => from.Client.Name))
               .ForMember(to => to.Status, opt => 
               opt.MapFrom(from => (from.StatusId == ProposalStatus.Approved)
               ? "Approved" : (from.StatusId == ProposalStatus.Rejected) ? "Rejected" : "Analysing"));
               




        }
    }
}
