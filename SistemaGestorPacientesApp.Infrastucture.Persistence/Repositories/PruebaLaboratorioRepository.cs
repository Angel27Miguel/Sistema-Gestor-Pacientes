using Microsoft.EntityFrameworkCore;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using SistemaGestorPacientesApp.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Infrastucture.Persistence.Repositories
{
    public class PruebaLaboratorioRepository : GenericRepository<PruebaLaboratorio>, IPruebaLaboratorioRepository
    {
        private readonly ApplicationContext _dbContext;

        public PruebaLaboratorioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PruebaLaboratorio> GetByIdWithIncludeAsync(int id, List<string> properties)
        {
            var query = _dbContext.Set<PruebaLaboratorio>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
