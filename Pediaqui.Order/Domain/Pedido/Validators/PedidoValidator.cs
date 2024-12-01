using Domain.Common.Ports;
using FluentValidation;

namespace Domain.Pedido.Validators;

public class PedidoValidator : IValidatorEntity<Entities.Pedido>
{
    public void Validate(Entities.Pedido entity)
    {
        var validationResult = new PedidoFluentValidator().Validate(entity);

        foreach (var error in validationResult.Errors)
        {
            entity.AddError(error.ErrorCode, error.ErrorMessage);
        }
    }
}
public class PedidoFluentValidator : AbstractValidator<Entities.Pedido>
{
    public PedidoFluentValidator()
    {
        RuleFor(p => p.Itens)
            .NotEmpty()
            .WithMessage("Não é possível criar um pedido sem itens");
    }
}
