using CashFlow.Domain.Repositors;
using CashFlow.Domain.Repositors.Expenses;
using CashFlow.Infraestrutura.DataAccess;
using CashFlow.Infraestrutura.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infraestrutura;

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
        services.AddScoped<IExpenseReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpenseWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpenseUpdateOnlyRepository, ExpensesRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connetion");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 46));

        services.AddDbContext<CashFlowDBContext>(config => config.UseMySql(connectionString, serverVersion));
    }

}
