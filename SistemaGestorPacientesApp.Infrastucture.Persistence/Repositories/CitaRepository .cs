using Microsoft.EntityFrameworkCore;
using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using SistemaGestorPacientesApp.Infrastructure.Persistence.Contexts;

namespace SistemaGestorPacientesApp.Infrastucture.Persistence.Repositories
{
    public class CitaRepository : GenericRepository<Cita>, ICitaRepository
    {
        private readonly ApplicationContext _dbContext;
        public CitaRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cita> GetCitaConDetallesAsync(int id)
        {
            return await _dbContext.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Consultorio)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
