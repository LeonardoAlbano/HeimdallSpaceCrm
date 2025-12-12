using FluentValidation;
using HeimdallSpaceCrm.Application.UseCases.Auth.Register;
using HeimdallSpaceCrm.Communication.Auth.Register.Requests;
using HeimdallSpaceCrm.Communication.Auth.Register.Responses;
using HeimdallSpaceCrm.Exception;
using Microsoft.AspNetCore.Mvc;

namespace HeimdallSpaceCrm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IRegisterUserUseCase _registerUserUseCase;

    public AuthController(IRegisterUserUseCase registerUserUseCase)
    {
        _registerUserUseCase = registerUserUseCase;
    }
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken ct)
    {
        try
        {
            var result = await _registerUserUseCase.ExecuteAsync(request, ct);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            var problem = new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation failed"
            };

            return BadRequest(problem);
        }
        catch (EmailAlreadyExistsException ex)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Email already in use",
                Detail = ex.Message
            };

            return Conflict(problem);
        }
        catch (System.Exception ex)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unexpected error",
                Detail = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, problem);
        }
    }
}
