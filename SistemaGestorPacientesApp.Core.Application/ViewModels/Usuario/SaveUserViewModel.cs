using System.ComponentModel.DataAnnotations;

public class SaveUserViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string Apellido { get; set; }


    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string NombreUsuario { get; set; }
    public string? NombreUsuarioError { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
    public string Correo { get; set; }

    [DataType(DataType.Password)]
    [RequiredIfCreating(ErrorMessage = "La contraseña es obligatoria para el registro.")]
    public string? Contraseña { get; set; }

    [DataType(DataType.Password)]
    [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
    public string? ConfirmarContraseña { get; set; }
    public bool EsAdministrador { get; set; }
    public int ConsultorioId { get; set; }
}
