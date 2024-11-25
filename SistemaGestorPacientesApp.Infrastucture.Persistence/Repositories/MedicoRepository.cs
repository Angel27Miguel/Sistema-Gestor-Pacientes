using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using SistemaGestorPacientesApp.Infrastructure.Persistence.Contexts;

namespace SistemaGestorPacientesApp.Infrastucture.Persistence.Repositories
{
    public class MedicoRepository : GenericRepository<Medico>, IMedicoRepository
    {
        private readonly ApplicationContext _dbContext;
        public MedicoRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    
    }
}
