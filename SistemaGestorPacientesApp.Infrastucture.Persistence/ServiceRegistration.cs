using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Infrastructure.Persistence.Contexts;
using SistemaGestorPacientesApp.Infrastucture.Persistence.Repositories;


namespace SistemaGestorPacientesApp.Infrastucture.Persistence
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            #region Contexts
            services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICitaRepository, CitaRepository>();
            services.AddTransient<IMedicoRepository, MedicoRepository>();
            services.AddTransient<IResultadoLaboratorioRepository, ResultadoLaboratorioRepository>();
            services.AddTransient<IPacienteRepository, PasienteRepository>();
            services.AddTransient<IPruebaLaboratorioRepository, PruebaLaboratorioRepository>();
            services.AddTransient<IConsultorioRepository, ConsultorioRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
