using CashFlow.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestrutura.DataAccess;

public class CashFlowDBContext : DbContext
{
    public CashFlowDBContext(DbContextOptions option) : base(option){}
    public DbSet<Expense> Expenses { get; set; }

    
}
