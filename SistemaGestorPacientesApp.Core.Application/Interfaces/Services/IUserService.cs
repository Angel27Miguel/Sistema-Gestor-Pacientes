using SistemaGestorPacientesApp.Core.Application.ViewModels.User;
using SistemaGestorPacientesApp.Core.Domain.Entities;

namespace SistemaGestorPacientesApp.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel vm);
    }
}
