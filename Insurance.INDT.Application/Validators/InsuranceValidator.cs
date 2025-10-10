using FluentValidation;
using Insurance.INDT.Dto.Request;

namespace Insurance.INDT.Application.Validators
{
    public class RegisterInsuranceDtoValidator : AbstractValidator<RegisterInsuranceDto>
    {
        public RegisterInsuranceDtoValidator()
        {
            RuleFor(client => client.Name).NotNull().NotEmpty().WithMessage("Nome obrigatório");
        }
    }
}
