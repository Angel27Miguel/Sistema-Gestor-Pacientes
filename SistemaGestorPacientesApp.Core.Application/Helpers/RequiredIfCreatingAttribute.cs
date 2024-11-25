using System.ComponentModel.DataAnnotations;

public class RequiredIfCreatingAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var userViewModel = (SaveUserViewModel)validationContext.ObjectInstance;

        // Se requiere contraseña solo si se esta creando un nuevo usuario
        if (userViewModel.Id == 0 && string.IsNullOrEmpty(value as string))
        {
            return new ValidationResult(ErrorMessage);
        }

        // Si se esta editando y se proporciona un valor vacio, no se requiere la contraseña
        return ValidationResult.Success;
    }
}
