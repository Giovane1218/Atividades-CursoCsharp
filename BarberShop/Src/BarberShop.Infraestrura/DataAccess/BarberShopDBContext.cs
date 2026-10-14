using BarberShop.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infraestrutura.DataAccess;

public class BarberShopDBContext : DbContext
{
    public BarberShopDBContext(DbContextOptions option) : base(option){}
    public DbSet<Fatura> faturamentos { get; set; }
}
