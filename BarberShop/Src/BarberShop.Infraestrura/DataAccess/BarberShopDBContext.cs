using BarberShop.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infraestrutura.DataAccess;

public class BarberShopDBContext : DbContext
{
    public BarberShopDBContext(DbContextOptions option) : base(option){}
    public DbSet<Fatura> Faturamentos { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tag>().ToTable("Tags");
    }
}
