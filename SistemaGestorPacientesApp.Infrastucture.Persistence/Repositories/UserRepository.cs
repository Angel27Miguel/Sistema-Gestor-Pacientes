using Microsoft.EntityFrameworkCore;
using SistemaGestorPacientesApp.Core.Application.Helpers;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Application.ViewModels.User;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using SistemaGestorPacientesApp.Infrastructure.Persistence.Contexts;

namespace SistemaGestorPacientesApp.Infrastucture.Persistence.Repositories
{
    public class UserRepository : GenericRepository<Usuario>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Usuario> AddAsync(Usuario entity)
        {
            entity.Contraseña = PasswordEncryptation.ComputeSha256Hash(entity.Contraseña);
            await base.AddAsync(entity);
            return entity;
        }

        public async Task<Usuario> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordEncryptation.ComputeSha256Hash(loginVm.Contraseña);
            Usuario user = await _dbContext.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.NombreUsuario == loginVm.NombreUsuario && u.Contraseña == passwordEncrypt);
            return user;
        }

        public async Task<bool> UserNameExistsAsync(string nombreUsuario)
        {
            return await _dbContext.Set<Usuario>()
                .AnyAsync(u => u.NombreUsuario == nombreUsuario);
        }
    }
}
