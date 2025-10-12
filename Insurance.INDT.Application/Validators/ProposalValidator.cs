using FluentValidation;
using INDT.Common.Insurance.Dto.Request;

namespace Insurance.INDT.Application.Validators
{
    public class RegisterProposalValidator : AbstractValidator<RegisterProposalDto>
    {
        public RegisterProposalValidator()
        {
            RuleFor(proposal => proposal.ClientId).GreaterThan(0).WithMessage("Codigo cliente deve ser valido");
            RuleFor(proposal => proposal.InsuranceId).GreaterThan(0).WithMessage("Codigo do seguro deve ser valido");
            RuleFor(proposal => proposal.Value).GreaterThan(0).WithMessage("Valor deve ser maior que zero");
            RuleFor(proposal => proposal.ExpirationDate).GreaterThan(System.DateTime.UtcNow).WithMessage("Data deve ser maior q atual");
        }
    }
}
