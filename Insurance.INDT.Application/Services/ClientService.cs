using AutoMapper;
using FluentValidation;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Dto.Request;
using INDT.Common.Insurance.Dto.Response;
using Insurance.INDT.Application.Services.Interfaces;

namespace Insurance.INDT.Application.Services
{
    public class ClientService: IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidator<RegisterClientDto> _clientValidator;

        private readonly IMapper _dataMapper;

        public ClientService(IClientRepository clientRepository, IValidator<RegisterClientDto> clientValidator, IMapper dataMapper)
        {
            _clientRepository = clientRepository;
            _clientValidator = clientValidator;
            _dataMapper = dataMapper;
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
                    
                    Client client = new Client( registerClient.Name, registerClient.Docto, registerClient.Age);

                    if (!await _clientRepository.Register(client))
                        return Result.Failure("999");

                    return Result.Success;

                }

                return Result.Failure("400", "Ja existe cliente com este documento");//Erro q usuario ja existe com este documento.
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
                ArgumentNullException.ThrowIfNullOrWhiteSpace(docto, "docto");


                Client client = await _clientRepository.GetByDocto(docto);

                if (client is null)
                    return Result.Failure("404"); //erro nao encontrado
                else
                {
                    var clientDto = _dataMapper.Map<ClientDto>(client);

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


            IList<ClientDto> list = clientList.Select(c =>_dataMapper.Map<ClientDto>(c)).ToList();

            return Result<IList<ClientDto>>.Success(list);
        }
    }
}
