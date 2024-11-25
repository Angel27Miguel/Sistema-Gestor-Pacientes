using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestorPacientesApp.Core.Application.ViewModels.PruebaLaboratorio
{
    public class ConsultarPruebasViewModel
    {
       
            public int CitaId { get; set; }
            public List<PruebaLaboratorioViewModel> PruebasLaboratorio { get; set; }
            public List<int> PruebasSeleccionadas { get; set; } 
        

    }

}
