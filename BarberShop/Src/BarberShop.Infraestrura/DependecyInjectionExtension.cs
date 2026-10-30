using BarberShop.Domain.Repositors;
using BarberShop.Domain.Repositors.Faturas;
using BarberShop.Domain.Repositors.User;
using BarberShop.Domain.Security.Cryptography;
using BarberShop.Domain.Security.Tokens;
using BarberShop.Domain.Services.LoggedUser;
using BarberShop.Infraestrura.DataAccess.Repositories;
using BarberShop.Infraestrura.Security.Token;
using BarberShop.Infraestrura.Services.LoggedUser;
using BarberShop.Infraestrutura.DataAccess;
using BarberShop.Infraestrutura.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Infraestrutura;

public static class DependecyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddToken(services, configuration);
        AddRepositories(services);

        services.AddScoped<ISenhaEncriptador, Infraestrura.Security.Cryptografy.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();
    }

    public static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expiration_time_minutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenarator>(config => new JwtTokenGenerator(expiration_time_minutes, signingKey!));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFaturaReadOnlyRepository, FaturasRepository>();
        services.AddScoped<IFaturaWriteOnlyRepository, FaturasRepository>();
        services.AddScoped<IFaturaUpdateOnlyRepository, FaturasRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connetion")
            ?? throw new InvalidOperationException("Connection String not found.");

        services.AddDbContext<BarberShopDBContext>(config => config.UseMySQL(connectionString));
    }

}
