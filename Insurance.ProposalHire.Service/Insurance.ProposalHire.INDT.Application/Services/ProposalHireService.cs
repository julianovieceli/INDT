using AutoMapper;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Domain.Interfaces.Repository;
using INDT.Common.Insurance.Dto.Request;
using INDT.Common.Insurance.Dto.Response;
using Insurance.ProposalHire.INDT.Application.Api;
using Insurance.ProposalHire.INDT.Application.Services.Interfaces;
using System.Net;
using Domain = INDT.Common.Insurance.Domain;

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

        public async Task<Result> Register(HireProposalDto hireProposalDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(hireProposalDto, "hireProposalDto");

                if (hireProposalDto.ProposalId <= 0)
                    throw new ArgumentOutOfRangeException();


                var proposalResponse = await _apiProposalService.GetProposalById(hireProposalDto.ProposalId);
                if(proposalResponse.IsFailure)
                    return Result.Failure(proposalResponse.ErrorCode, proposalResponse.ErrorMessage, (HttpStatusCode)proposalResponse.StatusCode);

                Proposal proposal = _dataMapper.Map<Proposal>(((Result<ProposalDto>)proposalResponse).Value);

                Domain.ProposalHire poposalHire = new Domain.ProposalHire(proposal, hireProposalDto.ExpirationDate, hireProposalDto.Description);

                if(await _proposalHireRepository.GetCounByProposalId(hireProposalDto.ProposalId) > 0)
                    return Result.Failure("400", "Proposta ja contratada", HttpStatusCode.BadRequest);

                if (!await _proposalHireRepository.Register(poposalHire))
                    return Result.Failure("999");

                    return Result.Success;

            }
            catch(Exception e)
            {
                return Result.Failure("999", e.Message, System.Net.HttpStatusCode.InternalServerError);
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
