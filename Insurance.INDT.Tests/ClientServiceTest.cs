using AutoFixture;
using AutoMapper;
using FluentValidation;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Domain.Interfaces.Infra;
using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Dto.Request;
using INDT.Common.Insurance.Dto.Response;
using Insurance.INDT.Application.Mapping;
using Insurance.INDT.Application.Services;
using Insurance.INDT.Application.Services.Interfaces;
using Moq;

namespace Insurance.Proposal.INDT.Tests
{
    public class ClientServiceTest
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly IMapper _dataMapper;
        private readonly Mock<IValidator<RegisterClientDto>> _registerClientDtoValidatorMock;
        private readonly Fixture _autofixture;
        private readonly Mock<IAzureMessagingClientService> _serviceBusClientServiceMock;
        private readonly Mock<IAWSMessagingClientService> _awsMessagingClientService;

        public ClientServiceTest()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _registerClientDtoValidatorMock = new Mock<IValidator<RegisterClientDto>>();
            _autofixture = new AutoFixture.Fixture ();
            _serviceBusClientServiceMock = new Mock<IAzureMessagingClientService>();
            _awsMessagingClientService = new Mock<IAWSMessagingClientService>();    

            if (_dataMapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ClientProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _dataMapper = mapper;
            }
        }

        [Fact]
        public void GetByDoctoTestNulArgumentException()
        {
            var clientCreated = _autofixture.Create<ClientDto>();

            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapper, _serviceBusClientServiceMock.Object, _awsMessagingClientService.Object);

     
            //Assert
            Assert.ThrowsAsync<ArgumentException>(() => clienteService.GetByDocto(""));

        }

        [Fact]
        public async Task GetByDoctoTestClientNotFound()
        {
            
            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapper,_serviceBusClientServiceMock.Object, _awsMessagingClientService.Object);

            _clientRepositoryMock
                .Setup(x => x.GetByDocto(It.IsAny<string>()))
                .Returns(Task.FromResult<Client>(null));



            var result = await clienteService.GetByDocto("umDocto");



            //Assert
            Assert.True(result.IsFailure);
            Assert.True(result.StatusCode == 400);
            Assert.True(result.ErrorCode == "404");

        }

        [Fact]
        public async Task GetByDoctoTestClientSuccess()
        {
            var clientCreated = _autofixture.Create<Client>();

            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapper, _serviceBusClientServiceMock.Object, _awsMessagingClientService.Object);

            _clientRepositoryMock
                .Setup(x => x.GetByDocto(It.IsAny<string>()))
                .Returns(Task.FromResult<Client>(clientCreated));
          

            var result = await clienteService.GetByDocto("umDocto");



            //Assert
            Assert.False(result.IsFailure);
            Assert.True(result.StatusCode == 200);
            Assert.True(((Result<ClientDto>)result).Value.Name == clientCreated.Name);
            Assert.True(((Result<ClientDto>)result).Value.Docto == clientCreated.Docto);


        }

        [Fact]
        public async Task GetAllTestClientSuccess()
        {
            var clientListCreated = _autofixture.CreateMany<Client>(5);

            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapper, _serviceBusClientServiceMock.Object, _awsMessagingClientService.Object);

            _clientRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(Task.FromResult(clientListCreated.ToList()));


            var resultList = await clienteService.GetAll();



            //Assert
            Assert.False(resultList.IsFailure);
            Assert.True(resultList.StatusCode == 200);

            Assert.True(((Result<IList<ClientDto>>)resultList).Value.Count == clientListCreated.Count());



        }

    }
}
