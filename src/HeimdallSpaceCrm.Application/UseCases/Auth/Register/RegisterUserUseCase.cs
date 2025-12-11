using FluentValidation;
using HeimdallSpaceCrm.Application.Security;
using HeimdallSpaceCrm.Communication.Auth.Register.Requests;
using HeimdallSpaceCrm.Communication.Auth.Register.Responses;
using HeimdallSpaceCrm.Domain.Users;
using HeimdallSpaceCrm.Exception;

namespace HeimdallSpaceCrm.Application.UseCases.Auth.Register;


public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IValidator<RegisterUserRequest> _validator;

    public RegisterUserUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IValidator<RegisterUserRequest> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;   
        _validator = validator;
    }

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request, CancellationToken ct = default)
    {
        var validation = await _validator.ValidateAsync(request, ct);
        if(!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var existing = await _userRepository.GetByEmailAsync(request.Email, ct);
        if(existing is not null)
            throw new EmailAlreadyExistsException(request.Password);
        
        var hash = _passwordHasher.Hash(request.Password);
        
        var user = User.Create(Guid.NewGuid(), request.Name, request.Email, hash);


        
        await _userRepository.AddAsync(user, ct);
        await _userRepository.SaveChangesAsync(ct);
        
        return new RegisterUserResponse(user.Id, user.Name, user.Email);
    }
}