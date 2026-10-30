using BarberShop.Domain.Entidades;
using BarberShop.Domain.Repositors.User;
using BarberShop.Infraestrutura.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infraestrura.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly BarberShopDBContext _dbContext;

    public UserRepository(BarberShopDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    public async Task Add(Usuario user)
    {
        await _dbContext.Usuario.AddAsync(user);
    }

    public async Task<bool> Delete(Usuario user)
    { 
        _dbContext.Usuario.Remove(user);

        return true;
    }

    public async Task<bool> ExisteUserComEmail(string email)
    {
        return await _dbContext.Usuario.AnyAsync(user => user.Email.Equals(email));
    }

    async Task<Usuario?> IUserReadOnlyRepository.GetUserById(long Id)
    {
        return await _dbContext.Usuario.AsNoTracking().FirstOrDefaultAsync(u =>u.Id == Id);
    }
    async Task<Usuario?> IUserUpdateOnlyRepository.GetUserById(long Id)
    {
        return await _dbContext.Usuario.AsNoTracking().FirstOrDefaultAsync(u =>u.Id == Id);
    }
    public void Update(Usuario user)
    {
        _dbContext.Usuario.Update(user);
    }
    public async Task<Usuario?> GetUserLogin(string email)
    {
        return await _dbContext.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email));
    }
}
