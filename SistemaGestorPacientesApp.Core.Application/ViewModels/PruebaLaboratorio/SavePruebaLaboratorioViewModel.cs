
using SistemaGestorPacientesApp.Core.Application.ViewModels.Consultorio;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestorPacientesApp.Core.Application.ViewModels.PruebaLaboratorio
{
    public class SavePruebaLaboratorioViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la prueba es requerido.")]
        [MaxLength(200, ErrorMessage = "El nombre de la prueba no puede exceder los 200 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El consultorio es requerido.")]
        public int? ConsultorioId { get; set; }
        public List<ConsultorioViewModel>? Consultorios { get; set; }

    }
}
