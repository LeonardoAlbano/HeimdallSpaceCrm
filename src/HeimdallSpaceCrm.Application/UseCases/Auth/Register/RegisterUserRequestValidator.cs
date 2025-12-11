using FluentValidation;
using HeimdallSpaceCrm.Communication.Auth.Register.Requests;

namespace HeimdallSpaceCrm.Application.Auth.Register.Requests;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3);
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
    
}