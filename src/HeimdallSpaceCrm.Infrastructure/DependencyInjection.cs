using FluentValidation;
using HeimdallSpaceCrm.Application.Security;
using HeimdallSpaceCrm.Application.UseCases.Auth.Register;
using HeimdallSpaceCrm.Communication.Auth.Register.Requests;
using HeimdallSpaceCrm.Domain.Users;
using HeimdallSpaceCrm.Infrastructure.Persistence;
using HeimdallSpaceCrm.Infrastructure.Persistence.Repositories;
using HeimdallSpaceCrm.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeimdallSpaceCrm.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<HeimdallDbContext>(options =>
            options.UseInMemoryDatabase("heimdall-spacecrm"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

        services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserRequestValidator>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        return services;
    }
}