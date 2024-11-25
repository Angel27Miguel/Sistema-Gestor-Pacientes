using Microsoft.AspNetCore.Http;
using SistemaGestorPacientesApp.Core.Application.ViewModels.Consultorio;
using SistemaGestorPacientesApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestorPacientesApp.Core.Application.ViewModels.Medico
{
    public class SaveMedicoViewModel
    {
        public int Id { get; set; }

            [Required(ErrorMessage = "El nombre es requerido.")]
            [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El apellido es requerido.")]
            [MaxLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
            public string Apellido { get; set; }

            [Required(ErrorMessage = "El correo es requerido.")]
            [EmailAddress(ErrorMessage = "El correo no es válido.")]
            [MaxLength(150, ErrorMessage = "El correo no puede exceder los 150 caracteres.")]
            public string Correo { get; set; }

            [Required(ErrorMessage = "El telefono es requerido.")]
            [MaxLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
            public string Telefono { get; set; }

            [Required(ErrorMessage = "La cedula es requerida.")]
            [MaxLength(15, ErrorMessage = "La cédula no puede exceder los 15 caracteres.")]
            public string Cedula { get; set; }

            public string? Foto { get; set; }

            [Required(ErrorMessage = "El consultorio es requerido.")]
            public int? ConsultorioId { get; set; }
            public List<ConsultorioViewModel>? Consultorio { get; set; }

            [DataType(DataType.Upload)]
            public IFormFile? File { get; set; }
    }

    
}
