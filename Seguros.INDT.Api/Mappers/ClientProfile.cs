using AutoMapper;
using Insurance.INDT.Domain;
using Insurance.INDT.Dto.Response;

namespace Insurance.Proposal.INDT.Api.Mappers
{
    public class ClientProfile: Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
