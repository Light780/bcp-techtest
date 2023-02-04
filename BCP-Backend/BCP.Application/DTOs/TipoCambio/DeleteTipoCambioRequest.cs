using FluentValidation;

namespace BCP.Application.DTOs.TipoCambio
{
    public class DeleteTipoCambioRequest
    {
        public string Id { get; set; }
    }
    
    public class DeleteTipoCambioValidator : AbstractValidator<DeleteTipoCambioRequest>
    {
        public DeleteTipoCambioValidator()
        {
            RuleFor(p => p.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Id es requerido")
                .NotEmpty().WithMessage("Id es requerido");

        }
    }
}