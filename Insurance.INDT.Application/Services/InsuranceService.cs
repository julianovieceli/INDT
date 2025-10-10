using AutoMapper;
using FluentValidation;
using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Domain;
using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Dto.Request;
using Insurance.INDT.Dto.Response;

namespace Insurance.INDT.Application.Services
{
    public class InsuranceService: IInsuranceService
    {
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IValidator<RegisterInsuranceDto> _insuranceValidator;

        private readonly IMapper _dataMapper;

        public InsuranceService(IInsuranceRepository insuranceRepository, IValidator<RegisterInsuranceDto> insuranceValidator, IMapper dataMapper)
        {
            _insuranceRepository = insuranceRepository;
            _insuranceValidator = _insuranceValidator;
            _dataMapper = dataMapper;
        }
        public async Task<Result> Register(RegisterInsuranceDto registerInsurance)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(registerInsurance, "registerInsurance");

                var validatorResult = _insuranceValidator.Validate(registerInsurance);
                if (!validatorResult.IsValid)
                {
                    return Result.Failure("400", validatorResult.Errors.FirstOrDefault().ErrorMessage);//Erro q usuario ja existe com este documento.
                }

                if (await _insuranceRepository.GetCountByName(registerInsurance.Name) == 0)
                {

                    Domain.Insurance insurance = new Domain.Insurance(registerInsurance.Name);

                    if (!await _insuranceRepository.Register(insurance))
                        return Result.Failure("999");

                    return Result.Success;

                }

                return Result.Failure("400");//Erro q seguro ja existe com este nome
            }
            catch
            {
                return Result.Failure("999", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result> GetByName(string name)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(name, "name");


                Domain.Insurance insurance = await _insuranceRepository.GetByName(name);

                if (insurance is null)
                    return Result.Failure("404"); //erro nao encontrado
                else
                {
                    var insuranceDto = _dataMapper.Map<InsuranceDto>(insurance);

                    return Result<InsuranceDto>.Success(insuranceDto);
                }
            }
            catch
            {
                return Result.Failure("999", System.Net.HttpStatusCode.InternalServerError);
            }
        }


        public async Task<Result> GetAll()
        {

            var clientList = await _insuranceRepository.GetAll();

            if (clientList is null)
                return Result.Failure("999");
            else if (clientList.Count == 0)
                return Result.Failure("404", System.Net.HttpStatusCode.NotFound);


            IList<InsuranceDto> list = clientList.Select(c => _dataMapper.Map<InsuranceDto>(c)).ToList();

            return Result<IList<InsuranceDto>>.Success(list);
        }
    }
}
