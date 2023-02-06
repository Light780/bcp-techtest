using System;
using FluentValidation;

namespace BCP.Application.DTOs.TipoCambio
{
    public class CreateTipoCambioRequest
    {
        public string Moneda { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Compra { get; set; }
        public decimal Venta { get; set; }
    }

    public class CreateTipoCambioValidator : AbstractValidator<CreateTipoCambioRequest>
    {
        public CreateTipoCambioValidator()
        {
            RuleFor(p => p.Moneda)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Moneda es requerido")
                .NotEmpty().WithMessage("Moneda es requerido")
                .Length(3).WithMessage("Moneda debe tener una longitud de 3 caracteres");

            RuleFor(p => p.Compra)
                .GreaterThan(0).WithMessage("Compra debe ser mayor a 0");
            
            RuleFor(p => p.Venta)
                .GreaterThan(0).WithMessage("Venta debe ser mayor a 0");
            
            RuleFor(p => p.Fecha)
                .LessThan(DateTime.Now).WithMessage("Fecha no puede ser mayor a la de hoy");

        }
    }
}