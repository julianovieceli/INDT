using AutoMapper;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Response;
using Insurance.INDT.Infra.MongoDb.Repository.Domain;

namespace Insurance.INDT.Application.Mapping
{
    public class ClientProfile: Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();

            CreateMap<Client, ClientDocument>().
            ForMember(dest => dest.Id, opt => opt.Ignore()).
            ReverseMap();
        }
    }
}
