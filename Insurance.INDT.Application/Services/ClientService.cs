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

            Client client = await _clientRepository.GetByDocto(registerClient.Docto);

            if (client is null)
            {

                string idClient = Guid.NewGuid().ToString();

                client = new Client(idClient, registerClient.Name, registerClient.Docto, registerClient.Age);

                if (!await _clientRepository.Register(client))
                    return Result.Failure("999");

                return Result.Success;

            }

            return Result.Failure("999");
        }

        public async Task<Result> GetByDocto(string docto)
        {
            ArgumentNullException.ThrowIfNull(docto, "docto");


            Client client = await _clientRepository.GetByDocto(docto);

            if (client is null)
                return Result.Failure("999");

            return Result<Client>.Success(client);
        }


        public async Task<Result> GetAll()
        {
            
            IList<Client> clientList = await _clientRepository.GetAll();

            if (clientList is null)
                return Result.Failure("999");

            return Result<IList<Client>>.Success(clientList);
        }
    }
}
