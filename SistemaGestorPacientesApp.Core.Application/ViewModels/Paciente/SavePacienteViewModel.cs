using Microsoft.AspNetCore.Http;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Consultorio;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestorPacientesApp.Core.Application.ViewModels.Paciente
{
    public class SavePacienteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido.")]
        [MaxLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El telefono es requerido.")]
        [MaxLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La direccion es requerida.")]
        [MaxLength(250, ErrorMessage = "La dirección no puede exceder los 250 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La cedula es requerida.")]
        [MaxLength(15, ErrorMessage = "La cédula no puede exceder los 15 caracteres.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de fecha incorrecto.")]
        public DateTime FechaNacimiento { get; set; }

        public bool EsFumador { get; set; }

        public string Alergias { get; set; }

        public string? Foto { get; set; }

        [Required(ErrorMessage = "El consultorio es requerido.")]
        public int ConsultorioId { get; set; }
        public List<ConsultorioViewModel>? Consultorio { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
