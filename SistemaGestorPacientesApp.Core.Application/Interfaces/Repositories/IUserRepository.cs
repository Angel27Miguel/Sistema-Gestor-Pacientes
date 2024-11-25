using SistemaGestorPacientesApp.Core.Application.ViewModels.User;
using SistemaGestorPacientesApp.Core.Domain.Entities;

namespace SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<Usuario>
    {
        Task<Usuario> LoginAsync(LoginViewModel loginVm);
        Task<bool> UserNameExistsAsync(string nombreUsuario);
    }
}
