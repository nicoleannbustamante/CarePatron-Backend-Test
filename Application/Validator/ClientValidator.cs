using Application.Models.Requests;
using FluentValidation;

namespace Application.Validator
{
    public class ClientValidator : AbstractValidator<ClientRequest>
    {
        public ClientValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please provide First Name");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Please provide Last Name");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Please provide Email");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Please provide Phone Number");
        }
    }
}
