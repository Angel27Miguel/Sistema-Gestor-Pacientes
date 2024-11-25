using Microsoft.AspNetCore.Http;
using SistemaGestorPacientesApp.Core.Application.ViewModels.User;
using SistemaGestorPacientesApp.Core.Application.Helpers;

namespace SistemaGestorPacientesApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            return userViewModel != null;
        }

        public int GetConsultorioId()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            return (int)(userViewModel != null ? userViewModel.ConsultorioId : 0); // Retorna 0 si no hay usuario logueado
        }
    }
}
