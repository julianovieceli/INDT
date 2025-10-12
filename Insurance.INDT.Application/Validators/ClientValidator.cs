using FluentValidation;
using INDT.Common.Insurance.Dto.Request;

namespace INDT.Common.Insurance.Application.Validators
{
    public class RegisterClientDtoValidator : AbstractValidator<RegisterClientDto>
    {
        public RegisterClientDtoValidator()
        {
            RuleFor(client => client.Name).NotNull().NotEmpty().WithMessage("Nome obrigatório");
            RuleFor(client => client.Age).GreaterThan(17).WithMessage("Age precisa ser maior que 18");
            RuleFor(client => client.Docto).NotNull().NotEmpty().WithMessage("Documento obrigatório");
        }
    }
}
