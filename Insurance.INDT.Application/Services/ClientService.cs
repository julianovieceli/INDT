using FluentValidation;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Domain;
using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Dto.Request;
using Insurance.INDT.Dto.Response;

namespace Insurance.INDT.Application.Services
{
    public class ClientService: IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidator<RegisterClientDto> _clientValidator;

        public ClientService(IClientRepository clientRepository, IValidator<RegisterClientDto> clientValidator)
        {
            _clientRepository = clientRepository;
            _clientValidator = clientValidator;
        }
        public async Task<Result> Register(RegisterClientDto registerClient )
        {
            try
            {
                ArgumentNullException.ThrowIfNull(registerClient, "registerClient");

                var validatorResult = _clientValidator.Validate(registerClient);
                if (!validatorResult.IsValid) 
                {
                    return Result.Failure("400", validatorResult.Errors.FirstOrDefault().ErrorMessage);//Erro q usuario ja existe com este documento.
                }

                if (await _clientRepository.GetCountByDocto(registerClient.Docto) == 0)
                {
                    string idClient = Guid.NewGuid().ToString();

                    Client client = new Client(idClient, registerClient.Name, registerClient.Docto, registerClient.Age);

                    if (!await _clientRepository.Register(client))
                        return Result.Failure("999");

                    return Result.Success;

                }

                return Result.Failure("400");//Erro q usuario ja existe com este documento.
            }
            catch
            {
                return Result.Failure("999", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result> GetByDocto(string docto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(docto, "docto");


                Client client = await _clientRepository.GetByDocto(docto);

                if (client is null)
                    return Result.Failure("404"); //erro nao encontrado
                else
                {
                    ClientDto clientDto = new ClientDto()
                    {
                        Id = client.Id,
                        Age = client.Age,
                        Docto = client.Docto,
                        Name = client.Name
                    };
                    return Result<ClientDto>.Success(clientDto);
                }
            }catch
            {
                return Result.Failure("999", System.Net.HttpStatusCode.InternalServerError);
            }
        }


        public async Task<Result> GetAll()
        {
            
            var clientList = await _clientRepository.GetAll();

            if (clientList is null)
                return Result.Failure("999");
            else if (clientList.Count == 0)
                return Result.Failure("404", System.Net.HttpStatusCode.NotFound);


            IList<ClientDto> list = clientList.Select(c => new ClientDto()
            {
                Id = c.Id,
                Age = c.Age,
                Docto = c.Docto,
                Name = c.Name
            }).ToList();

            return Result<IList<ClientDto>>.Success(list);
        }
    }
}
