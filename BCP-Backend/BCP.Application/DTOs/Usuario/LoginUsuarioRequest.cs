using FluentValidation;

namespace BCP.Application.DTOs.Usuario
{
    public class LoginUsuarioRequest
    {
        public string Correo { get; set; }
        public string Password { get; set; }
    }
    
    public class LoginUsuarioValidator : AbstractValidator<LoginUsuarioRequest>
    {
        public LoginUsuarioValidator()
        {

            RuleFor(p => p.Correo)
                .NotNull().WithMessage("Correo no puede ser nulo")
                .EmailAddress().WithMessage("Correo debe ser un email válido");

            RuleFor(p => p.Password)
                .NotNull().WithMessage("Password no puede ser nulo");
        }
    }
}