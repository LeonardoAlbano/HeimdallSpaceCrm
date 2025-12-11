using HeimdallSpaceCrm.Communication.Auth.Register.Requests;
using HeimdallSpaceCrm.Communication.Auth.Register.Responses;

namespace HeimdallSpaceCrm.Application.UseCases.Auth.Register;

public interface IRegisterUserUseCase
{
    Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request, CancellationToken ct = default);
}