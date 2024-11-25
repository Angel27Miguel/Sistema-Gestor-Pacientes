using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using SistemaGestorPacientesApp.Infrastructure.Persistence.Contexts;

namespace SistemaGestorPacientesApp.Infrastucture.Persistence.Repositories
{
    public class ConsultorioRepository : GenericRepository<Consultorio>, IConsultorioRepository
    {
        private readonly ApplicationContext _dbContext;
        public ConsultorioRepository(ApplicationContext dbContext) : base(dbContext)
        {
        _dbContext = dbContext;
        }
    
    }
}
