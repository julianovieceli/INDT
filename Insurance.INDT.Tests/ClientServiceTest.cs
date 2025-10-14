using AutoFixture;
using AutoMapper;
using FluentValidation;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Dto.Request;
using INDT.Common.Insurance.Dto.Response;
using Insurance.INDT.Application.Services;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Application.Mapping;
using Moq;
using Insurance.INDT.Application.ServiceBus;

namespace Insurance.Proposal.INDT.Tests
{
    public class ClientServiceTest
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly IMapper _dataMapper;
        private readonly Mock<IValidator<RegisterClientDto>> _registerClientDtoValidatorMock;
        private readonly Fixture _autofixture;
        private readonly Mock<IServiceBusClientService> _serviceBusClientServiceMock;

        public ClientServiceTest()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _registerClientDtoValidatorMock = new Mock<IValidator<RegisterClientDto>>();
            _autofixture = new AutoFixture.Fixture ();
            _serviceBusClientServiceMock = new Mock<IServiceBusClientService>();

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
                _dataMapper, _serviceBusClientServiceMock.Object);

     
            //Assert
            Assert.ThrowsAsync<ArgumentException>(() => clienteService.GetByDocto(""));

        }

        [Fact]
        public async Task GetByDoctoTestClientNotFound()
        {
            
            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapper,_serviceBusClientServiceMock.Object);

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
                _dataMapper, _serviceBusClientServiceMock.Object);

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
                _dataMapper, _serviceBusClientServiceMock.Object);

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
