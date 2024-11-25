using SistemaGestorPacientesApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestorPacientesApp.Core.Application.ViewModels.Consultorio
{
    public class SaveConsultorioViewModel
    {
        [Required(ErrorMessage = "El nombre del consultorio es requerido.")]
        [MaxLength(150, ErrorMessage = "El nombre del consultorio no puede exceder los 150 caracteres.")]
        public string Nombre { get; set; }
        public int Id { get; set; }
    }
}
