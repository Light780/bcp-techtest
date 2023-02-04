using FluentValidation;

namespace BCP.Application.DTOs.TipoCambio
{
    public class ConvertTipoCambioRequest
    {
        public decimal Monto { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
    }

    public class ConvertTipoCambioValidator : AbstractValidator<ConvertTipoCambioRequest>
    {
        public ConvertTipoCambioValidator()
        {
            RuleFor(p => p.Monto)
                .GreaterThan(0).WithMessage("Monto debe ser mayor a 0");

            RuleFor(p => p.MonedaOrigen)
                .NotNull().WithMessage("MonedaOrigen es requerido")
                .Length(3).WithMessage("MonedaOrigen debe tener una longitud de 3 caracteres");
            
            RuleFor(p => p.MonedaDestino)
                .NotNull().WithMessage("MonedaDestino no puede ser nulo")
                .Length(3).WithMessage("MonedaDestino debe tener una longitud de 3 caracteres");
        }
    }
}