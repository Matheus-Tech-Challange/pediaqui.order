using Domain.Common.Ports;
using Domain.Entities;
using FluentValidation;

namespace Domain.Pedido.Validators;

public class PedidoItemValidator : IValidatorEntity<PedidoItem>
{
    public void Validate(PedidoItem entity)
    {
        var validationResult = new PedidoItemFluentValidator().Validate(entity);

        foreach (var error in validationResult.Errors)
        {
            entity.AddError(error.ErrorCode, error.ErrorMessage);
        }
    }
}

public class PedidoItemFluentValidator : AbstractValidator<PedidoItem>
{
    public PedidoItemFluentValidator()
    {
        RuleFor(p => p.ProdutoId)
            .NotEmpty()
            .WithMessage("É necessário informar um produto válido");

        RuleFor(p => p.Quantidade)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Quantidade deve ser maior do que zero");

        RuleFor(p => p.Preco)
            .GreaterThan(0)
            .WithMessage("O preço deve ser maior que zero");

        RuleFor(p => p.Observacao)
            .MaximumLength(255)
            .WithMessage("Tamanho máximo para observaçao é de 255 caracteres");
    }
}
