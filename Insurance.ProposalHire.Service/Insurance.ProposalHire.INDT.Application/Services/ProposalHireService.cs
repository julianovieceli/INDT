using AutoMapper;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Dto.Response;
using Insurance.ProposalHire.INDT.Application.Api;
using Insurance.ProposalHire.INDT.Application.Services.Interfaces;
using System.Net;

namespace Insurance.INDT.Application.Services
{
    public class ProposalHireService: IProposalHireService
    {
        private readonly IProposalHireRepository _proposalHireRepository;
     
        private readonly IMapper _dataMapper;

        private readonly IApiProposalService _apiProposalService;


        public ProposalHireService(IApiProposalService apiProposalService, IProposalHireRepository proposalHireRepository, IMapper dataMapper)
        {
            _proposalHireRepository = proposalHireRepository;
            _apiProposalService = apiProposalService;   
            _dataMapper = dataMapper;
        }

        public async Task<Result> Register(int proposalId)
        {
            try
            {
                if (proposalId <= 0)
                    throw new ArgumentOutOfRangeException();


                var proposalResponse = await _apiProposalService.GetProposalById(proposalId);
                if(proposalResponse.IsFailure)
                    return Result.Failure(proposalResponse.ErrorCode, proposalResponse.ErrorMessage, (HttpStatusCode)proposalResponse.StatusCode);


                //var validatorResult = _proposalValidator.Validate(proposalDto);
                //if (!validatorResult.IsValid)
                //{
                //    return Result.Failure("400", validatorResult.Errors.FirstOrDefault().ErrorMessage);//Erro q usuario ja existe com este documento.
                //}

                //if (await _proposalRepository.GetByClientIdAndInsuranceId(proposalDto.ClientId, proposalDto.InsuranceId) is null)
                //{
                //    InsuranceDomain.Insurance insurance = await _insuranceRepository.GetById(proposalDto.InsuranceId);

                //    if (insurance is null)
                //        return Result.Failure("404", "Seguro não encontrado");

                //    Client client = await _clientRepository.GetById(proposalDto.ClientId);

                //    if (client is null)
                //        return Result.Failure("404", "Cliente não encontrado");

                //    Proposal proposal = new Proposal(insurance, client, proposalDto.ExpirationDate, proposalDto.Value);

                //    if (!await _proposalRepository.Register(proposal))
                //        return Result.Failure("999");

                    return Result.Success;

            //    }

                //return Result.Failure("400", "Ja existe proposta deste seguro para este cliente");//Erro q seguro ja existe com este nome
            }
            catch
            {
                return Result.Failure("999", System.Net.HttpStatusCode.InternalServerError);
            }
        }



        public async Task<Result> GetAll()
        {

            var proposalList = await _proposalHireRepository.GetAll();

            if (proposalList is null)
                return Result.Failure("999");
            else if (proposalList.Count == 0)
                return Result.Failure("404", System.Net.HttpStatusCode.NotFound);


            IList<ProposalHireDto> list = proposalList.Select(c => _dataMapper.Map<ProposalHireDto>(c)).ToList();

            return Result<IList<ProposalHireDto>>.Success(list);
        }



    }
}
