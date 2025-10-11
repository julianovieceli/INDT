using AutoMapper;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Domain;
using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Dto.Response;

namespace Insurance.INDT.Application.Services
{
    public class ProposalService: IProposalService
    {
        private readonly IProposalRepository _proposalRepository;
        //private readonly IValidator<RegisterInsuranceDto> _insuranceValidator;

        private readonly IMapper _dataMapper;

        public ProposalService(IProposalRepository proposalRepository, IMapper dataMapper
            //IValidator<RegisterInsuranceDto> insuranceValidator, 
            )
        {
            _proposalRepository = proposalRepository;
            //_insuranceValidator = insuranceValidator;
            _dataMapper = dataMapper;
        }
        //public async Task<Result> Register(RegisterInsuranceDto registerInsurance)
        //{
        //    try
        //    {
        //        ArgumentNullException.ThrowIfNull(registerInsurance, "registerInsurance");

        //        var validatorResult = _insuranceValidator.Validate(registerInsurance);
        //        if (!validatorResult.IsValid)
        //        {
        //            return Result.Failure("400", validatorResult.Errors.FirstOrDefault().ErrorMessage);//Erro q usuario ja existe com este documento.
        //        }

        //        if (await _insuranceRepository.GetCountByName(registerInsurance.Name) == 0)
        //        {

        //            Domain.Insurance insurance = new Domain.Insurance(registerInsurance.Name);

        //            if (!await _insuranceRepository.Register(insurance))
        //                return Result.Failure("999");

        //            return Result.Success;

        //        }

        //        return Result.Failure("400", "Ja existe seguro com este nome");//Erro q seguro ja existe com este nome
        //    }
        //    catch
        //    {
        //        return Result.Failure("999", System.Net.HttpStatusCode.InternalServerError);
        //    }
        //}

        public async Task<Result> GetById(int id)
        {
            try
            {
                if(id <= 0)
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


        //public async Task<Result> GetAll()
        //{

        //    var insuranceList = await _insuranceRepository.GetAll();

        //    if (insuranceList is null)
        //        return Result.Failure("999");
        //    else if (insuranceList.Count == 0)
        //        return Result.Failure("404", System.Net.HttpStatusCode.NotFound);


        //    IList<InsuranceDto> list = insuranceList.Select(c => _dataMapper.Map<InsuranceDto>(c)).ToList();

        //    return Result<IList<InsuranceDto>>.Success(list);
        //}
    }
}
