using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Services;
using SistemaGestorPacientesApp.Core.Application.Services;

namespace SistemaGestorPacientesApp.Core.Application
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services,IConfiguration configuration)
        {
            #region Services
            services.AddTransient<ICitaService, CitaService>();
            services.AddTransient<IMedicoService, MedicoService>();
            services.AddTransient<IResultadoLaboratorioService, ResultadoLaboratorioService>();
            services.AddTransient<IPacienteService, PacienteService>();
            services.AddTransient<IPruebaLaboratorioService, PruebaLaboratorioService>();
            services.AddTransient<IConsultorioService, ConsultorioService>();
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
