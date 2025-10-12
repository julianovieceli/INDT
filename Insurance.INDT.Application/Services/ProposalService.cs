using AutoMapper;
using FluentValidation;
using INDT.Common.Insurance.Dto.Request;
using INDT.Common.Insurance.Dto.Response;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Domain;
using Insurance.INDT.Domain.Enums;
using Insurance.INDT.Domain.Interfaces.Repository;

namespace Insurance.INDT.Application.Services
{
    public class ProposalService: IProposalService
    {
        private readonly IProposalRepository _proposalRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IValidator<RegisterProposalDto> _proposalValidator;

        private readonly IMapper _dataMapper;

        public ProposalService(IProposalRepository proposalRepository, IInsuranceRepository insuranceRepository,
            IClientRepository clientRepository,
        IMapper dataMapper,
            IValidator<RegisterProposalDto> proposalValidator)
        {
            _proposalRepository = proposalRepository;
            _insuranceRepository = insuranceRepository;
            _clientRepository = clientRepository;
            _proposalValidator = proposalValidator;
            _dataMapper = dataMapper;
        }
        public async Task<Result> Register(RegisterProposalDto proposalDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(proposalDto, "proposal");

                var validatorResult = _proposalValidator.Validate(proposalDto);
                if (!validatorResult.IsValid)
                {
                    return Result.Failure("400", validatorResult.Errors.FirstOrDefault().ErrorMessage);//Erro q usuario ja existe com este documento.
                }

                if (await _proposalRepository.GetByClientIdAndInsuranceId(proposalDto.ClientId, proposalDto.InsuranceId) is null)
                {
                    Domain.Insurance insurance = await _insuranceRepository.GetById(proposalDto.InsuranceId);

                    if(insurance is null)
                        return Result.Failure("404", "Seguro não encontrado");

                    Domain.Client client = await _clientRepository.GetById(proposalDto.ClientId);

                    if (client is null)
                        return Result.Failure("404", "Cliente não encontrado");

                    Domain.Proposal proposal = new Domain.Proposal(insurance, client, proposalDto.ExpirationDate, proposalDto.Value);

                    if (!await _proposalRepository.Register(proposal))
                        return Result.Failure("999");

                    return Result.Success;

                }

                return Result.Failure("400", "Ja existe proposta deste seguro para este cliente");//Erro q seguro ja existe com este nome
            }
            catch
            {
                return Result.Failure("999", System.Net.HttpStatusCode.InternalServerError);
            }
        }


        public async Task<Result> UpdateStatus(UpdateProposalDto updateProposalDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(nameof(updateProposalDto));

                if (updateProposalDto.ProposalId <= 0)
                    return Result.Failure("400", "ProposalId inválido");


                bool isValidStatus = Enum.IsDefined(typeof(ProposalStatus), updateProposalDto.StatusId);
                if (!isValidStatus)
                    return Result.Failure("400", "Status inválido");

                if ((ProposalStatus)updateProposalDto.StatusId == ProposalStatus.Analysing)
                    return Result.Failure("400", "Só é possivel aprovar ou rejeitar uma proposta");

                var proposal = await _proposalRepository.GetById(updateProposalDto.ProposalId);

                if(proposal is null)
                    return Result.Failure("404", "Proposta não encontrada");

                if(proposal.StatusId != ProposalStatus.Analysing)
                    return Result.Failure("400", "Proposta precisa estar no status 'Em Analise' para poder ser modificada");

                
                if (!await _proposalRepository.UpdateStatus(updateProposalDto.ProposalId, (ProposalStatus)updateProposalDto.StatusId))
                    return Result.Failure("999");

                return Result.Success;

            }
            catch
            {
                return Result.Failure("999", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException();

                Domain.Proposal proposal = await _proposalRepository.GetById(id);

                if (proposal is null)
                    return Result.Failure("404"); //erro nao encontrado
                else
                {
                    var proposalDto = _dataMapper.Map<ProposalDto>(proposal);

                    return Result<ProposalDto>.Success(proposalDto);
                }
            }
            catch
            {
                return Result.Failure("999", System.Net.HttpStatusCode.InternalServerError);
            }
        }


        public async Task<Result> GetAll()
        {

            var proposalList = await _proposalRepository.GetAll();

            if (proposalList is null)
                return Result.Failure("999");
            else if (proposalList.Count == 0)
                return Result.Failure("404", System.Net.HttpStatusCode.NotFound);


            IList<ProposalDto> list = proposalList.Select(c => _dataMapper.Map<ProposalDto>(c)).ToList();

            return Result<IList<ProposalDto>>.Success(list);
        }



    }
}
