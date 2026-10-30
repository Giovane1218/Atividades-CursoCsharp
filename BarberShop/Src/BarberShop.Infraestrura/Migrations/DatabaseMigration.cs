using BarberShop.Infraestrutura.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Infraestrura.Migrations;

public class DatabaseMigration
{
    public async static Task MigrationDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetService<BarberShopDBContext>();

        await dbContext!.Database.MigrateAsync();
    }
}
