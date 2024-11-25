using SistemaGestorPacientesApp.Core.Application.Interfaces.Repositories;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using SistemaGestorPacientesApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Infrastucture.Persistence.Repositories
{
    public class PasienteRepository : GenericRepository<Paciente>, IPacienteRepository
    {
        private readonly ApplicationContext _dbContext;
        public PasienteRepository (ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
