using INDT.Common.Insurance.Domain;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Infra.MongoDb.Repository.Domain;
using Insurance.INDT.Infra.MongoDb.Repository.Interface;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Insurance.INDT.Application.Services
{
    public class ClientDomainService: IClientDomainService
    {
        private readonly IClientDocumentRepository _clientDocumentRepository;

        private readonly ILogger<ClientDomainService> _logger;

        public ClientDomainService(ILogger<ClientDomainService> logger, IClientDocumentRepository clientDocumentRepository)
        {
            _clientDocumentRepository = clientDocumentRepository;
            _logger = logger;
        }


        public async Task<Result> InsertClient(ClientDocument clientDocument)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(clientDocument, "clientDocument");

                if (await _clientDocumentRepository.GetByDocto(clientDocument.Docto) is null)
                {

                    if(await _clientDocumentRepository.InsertAsync(clientDocument))
                    {
                        _logger.LogInformation($"Cliente {clientDocument.Name} inserido com sucesso no MongoDb");

                        return Result.Success;

                    }
                    return Result.Failure("400", "Ocorreram erros ao inserir no mongo");//Erro q usuario ja existe com este documento.

                }

                return Result.Failure("400", "Ja existe cliente com este documento no mongo");//Erro q usuario ja existe com este documento.
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return Result.Failure("999", System.Net.HttpStatusCode.InternalServerError);
        
            }
        }
    }
}
