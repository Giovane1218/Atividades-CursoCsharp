using BarberShop.Domain.Repositors;
using BarberShop.Domain.Repositors.Faturas;
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
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFaturaReadOnlyRepository, FaturasRepository>();
        services.AddScoped<IFaturaWriteOnlyRepository, FaturasRepository>();
        services.AddScoped<IFaturaUpdateOnlyRepository, FaturasRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connetion");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 46));

        services.AddDbContext<BarberShopDBContext>(config => config.UseMySql(connectionString, serverVersion));
    }

}
