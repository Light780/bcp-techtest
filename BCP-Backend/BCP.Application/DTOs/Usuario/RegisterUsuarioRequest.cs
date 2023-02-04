using FluentValidation;

namespace BCP.Application.DTOs.Usuario
{
    public class RegisterUsuarioRequest
    {
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUsuarioValidator : AbstractValidator<RegisterUsuarioRequest>
    {
        public RegisterUsuarioValidator()
        {
            RuleFor(p => p.NombreCompleto)
                .NotNull().WithMessage("NombreCompleto no puede ser nulo")
                .Length(8, 50).WithMessage("NombreCompleto debe tener una longitud entre 15 a 50 caracteres");

            RuleFor(p => p.Correo)
                .NotNull().WithMessage("Correo no puede ser nulo")
                .Length(10,50).WithMessage("Correo debe tener una longitud entre 10 a 50 caracteres")
                .EmailAddress().WithMessage("Correo debe ser un email válido");

            RuleFor(p => p.Password)
                .NotNull().WithMessage("Password no puede ser nulo")
                .Length(8, 32).WithMessage("Password debe tener una longitud entre 8 y 32 caracteres");
        }
    }
}