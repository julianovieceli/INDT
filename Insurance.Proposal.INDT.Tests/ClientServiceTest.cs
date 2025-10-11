using AutoFixture;
using AutoMapper;
using FluentValidation;
using Insurance.INDT.Application.Services;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Dto.Request;
using Insurance.INDT.Dto.Response;
using Moq;
using System.Runtime.CompilerServices;

namespace Insurance.Proposal.INDT.Tests
{
    public class ClientServiceTest
    {
        //private readonly Mock<IClientService> _clientServiceMock;
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IMapper> _dataMapperMock;
        private readonly Mock<IValidator<RegisterClientDto>> _registerClientDtoValidatorMock;
        private readonly Fixture _autofixture;

        public ClientServiceTest()
        {
            //_clientServiceMock = new Mock<IClientService> ();
            _clientRepositoryMock = new Mock<IClientRepository>();
            _registerClientDtoValidatorMock = new Mock<IValidator<RegisterClientDto>>();
            _dataMapperMock = new Mock<IMapper>();
            _autofixture = new AutoFixture.Fixture ();
        }

        [Fact]
        public void GetByDoctoTestNulArgumentException()
        {
            var clientCreated = _autofixture.Create<ClientDto>();

            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapperMock.Object );

     
            //Assert
            Assert.ThrowsAsync<ArgumentException>(() => clienteService.GetByDocto(""));

        }

        [Fact]
        public async Task GetByDoctoTestClientNotFound()
        {
            var clientCreated = _autofixture.Create<ClientDto>();

            IClientService clienteService = new ClientService(_clientRepositoryMock.Object, _registerClientDtoValidatorMock.Object,
                _dataMapperMock.Object);

            _clientRepositoryMock
                .Setup(x => x.GetByDocto(It.IsAny<string>()))
                .Returns(Task.FromResult<Insurance.INDT.Domain.Client>(null));

            var result = await clienteService.GetByDocto("umDocto");



            //Assert
            Assert.True(result.IsFailure);
            Assert.True(result.StatusCode == 400);
            Assert.True(result.ErrorCode == "404");

        }

    }
}