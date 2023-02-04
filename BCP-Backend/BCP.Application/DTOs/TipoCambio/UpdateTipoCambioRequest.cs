using System;
using FluentValidation;

namespace BCP.Application.DTOs.TipoCambio
{
    public class UpdateTipoCambioRequest
    {
        public string Id { get; set; }
        public decimal Compra { get; set; }
        public decimal Venta { get; set; }
    }
    
    public class UpdateTipoCambioValidator : AbstractValidator<UpdateTipoCambioRequest>
    {
        public UpdateTipoCambioValidator()
        {
            RuleFor(p => p.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Id es requerido")
                .NotEmpty().WithMessage("Id es requerido");

            RuleFor(p => p.Compra)
                .GreaterThan(0).WithMessage("Compra debe ser mayor a 0");
            
            RuleFor(p => p.Venta)
                .GreaterThan(0).WithMessage("Compra debe ser mayor a 0");

        }
    }
}