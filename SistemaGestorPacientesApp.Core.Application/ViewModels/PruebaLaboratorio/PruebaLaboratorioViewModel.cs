﻿

namespace SistemaGestorPacientesApp.Core.Application.ViewModels.PruebaLaboratorio
{
    public class PruebaLaboratorioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? ConsultorioId { get; set; }
        public string NombreConsultorio { get; set; }
    }
}