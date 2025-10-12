using AutoMapper;
using INDT.Common.Insurance.Dto.Response;
using Insurance.INDT.Domain;

namespace Insurance.Proposal.INDT.Application.Mapping
{
    public class ClientProfile: Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
