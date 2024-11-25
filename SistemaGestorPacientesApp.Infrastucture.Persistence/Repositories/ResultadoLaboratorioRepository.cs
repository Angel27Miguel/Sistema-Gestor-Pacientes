using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using SistemaGestorPacientesApp.Infrastructure.Persistence.Contexts;

namespace SistemaGestorPacientesApp.Infrastucture.Persistence.Repositories
{
    internal class ResultadoLaboratorioRepository : GenericRepository<ResultadoLaboratorio>, IResultadoLaboratorioRepository
    {
        private readonly ApplicationContext _dbContext;
        public ResultadoLaboratorioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}