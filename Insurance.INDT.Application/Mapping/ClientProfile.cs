using AutoMapper;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Response;

namespace Insurance.INDT.Application.Mapping
{
    public class ClientProfile: Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
