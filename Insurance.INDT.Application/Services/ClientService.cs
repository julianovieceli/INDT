using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Domain;
using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Dto.Request;

namespace Insurance.INDT.Application.Services
{
    public class ClientService: IClientService
    {
        private readonly IClientRepository _clientRepository;


        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<Result> Register(RegisterClientDto registerClient)
        {
            ArgumentNullException.ThrowIfNull(registerClient, "registerClient");

            if (!await _clientRepository.GetByDocto(registerClient.Docto))
            {

                string idClient = Guid.NewGuid().ToString();

                Client client = new Client(idClient, registerClient.Name, registerClient.Docto, registerClient.Age);

                return Result.Success;
            }

            return Result.Failure("999");
        }
    }
}
