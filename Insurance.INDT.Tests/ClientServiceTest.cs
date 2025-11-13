using AutoFixture;
using AutoMapper;
using FluentValidation;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Dto.Request;
using INDT.Common.Insurance.Dto.Response;
using INDT.Common.Insurance.Infra.Interfaces.AWS;
using INDT.Common.Insurance.Infra.Interfaces.Azure;
using INDT.Common.Insurance.Infra.Interfaces.Rabbit;
using Insurance.INDT.Application.Mapping;
using Insurance.INDT.Application.Services;
using Insurance.INDT.Application.Services.Interfaces;
using Moq;
using Personal.Common.Domain;
using Personal.Common.Domain.Interfaces.Services.Message;

namespace Insurance.Proposal.INDT.Tests
{
    public class ClientServiceTest
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly IMapper _dataMapper;
        private readonly Mock<IValidator<RegisterClientDto>> _registerClientDtoValidatorMock;
        private readonly Fixture _autofixture;
        private readonly Mock<IAzureMessagingClientStrategyService> _serviceBusClientServiceMock;
        private readonly Mock<IAWSMessagingClientStrategyService> _awsMessagingClientService;
        private readonly Mock<IRabbitMqMessagingClientStrategyService> _rabbitMqMessagingClientStrategyService;
        private readonly Mock<IMessageTopicService> _messageTopicMockService;



        private readonly IList<string> CPFList = new List<string>
        {
            "363.560.390-27",
            "625.340.220-08",
            "694.572.870-61",
            "074.741.230-88",
            "203.859.280-25",
        };
        public ClientServiceTest()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _registerClientDtoValidatorMock = new Mock<IValidator<RegisterClientDto>>();
            _autofixture = new AutoFixture.Fixture ();
            _serviceBusClientServiceMock = new Mock<IAzureMessagingClientStrategyService>();
            _awsMessagingClientService = new Mock<IAWSMessagingClientStrategyService>();
            _rabbitMqMessagingClientStrategyService = new Mock<IRabbitMqMessagingClientStrategyService>();
            _messageTopicMockService = new Mock<IMessageTopicService>();

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
                _dataMapper, _serviceBusClientServiceMock.Object, _awsMessagingClientService.Object, _rabbitMqMessagingClientStrategyService.Object,
                _messageTopicMockService.Object);

     
            //Assert
            Assert.ThrowsAsync<ArgumentException>(() => clienteService.GetByDocto(""));

        }

        [Fact]
        public async Task GetByDoctoTestClientNotFound()
        {
            
            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapper,_serviceBusClientServiceMock.Object, _awsMessagingClientService.Object, _rabbitMqMessagingClientStrategyService.Object,
                _messageTopicMockService.Object);

            _clientRepositoryMock
                .Setup(x => x.GetByDocto(It.IsAny<string>()))
                .Returns(Task.FromResult<Client>(null));



            var result = await clienteService.GetByDocto("umDocto");



            //Assert
            Assert.True(result.IsFailure);
            Assert.True(result.ErrorCode == "404");

        }

        [Fact]
        public async Task GetByDoctoTestClientSuccess()
        {
            string CPF = CPFList.First();

            _autofixture.Customize<Client>(composer =>
            composer.With(p => p.Docto, CPFList.First()));

            var clientCreated = _autofixture.Create<Client>();

            
            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapper, _serviceBusClientServiceMock.Object, _awsMessagingClientService.Object, _rabbitMqMessagingClientStrategyService.Object,
                _messageTopicMockService.Object);

            _clientRepositoryMock
                .Setup(x => x.GetByDocto(CPF))
                .Returns(Task.FromResult<Client>(clientCreated));
          

            var result = await clienteService.GetByDocto(CPF);



            //Assert
            Assert.False(result.IsFailure);
            Assert.True(((Result<ClientDto>)result).Response.Name == clientCreated.Name);
            Assert.True(((Result<ClientDto>)result).Response.Docto == clientCreated.Docto);


        }

        [Fact]
        public async Task RegisterClientInvalidDoctoException()
        {


        
            string CPF = "168.483";

            _autofixture.Customize<RegisterClientDto>(composer =>
            composer.With(p => p.Docto, CPF)
            .With(p => p.Age, 30));

            var clientToRegister = _autofixture.Create<RegisterClientDto>();


            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapper, _serviceBusClientServiceMock.Object, _awsMessagingClientService.Object, _rabbitMqMessagingClientStrategyService.Object,
                _messageTopicMockService.Object);

            _clientRepositoryMock
                           .Setup(x => x.GetByDocto(CPF))
                           .Returns(Task.FromResult<Client?>(null));

            _registerClientDtoValidatorMock.Setup(x => x.Validate(clientToRegister)).
                Returns(new FluentValidation.Results.ValidationResult());


            //Action
            var result = await clienteService.Register(clientToRegister);


            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains("Invalid Document", result.ErrorMessage);



        }

        [Fact]
        public async Task GetAllTestClientSuccess()
        {

            Random random = new Random();
            
   
            _autofixture.Customize<Client>(composer =>
      composer.With(x => x.Docto, CPFList[random.Next(CPFList.Count)]));

            var clientListCreated = _autofixture.CreateMany<Client>(5);



            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapper, _serviceBusClientServiceMock.Object, _awsMessagingClientService.Object, _rabbitMqMessagingClientStrategyService.Object,
                _messageTopicMockService.Object);

            _clientRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(Task.FromResult(clientListCreated.ToList()));


            var resultList = await clienteService.GetAll();



            //Assert
            Assert.False(resultList.IsFailure);
            
            Assert.True(((Result<IList<ClientDto>>)resultList).Response.Count == clientListCreated.Count());



        }

    }
}
