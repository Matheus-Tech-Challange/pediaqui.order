using Domain.Common.Ports;
using Domain.Pedido.Validators;

namespace Domain.Pedido.Factories;

public static class PedidoValidatorFactory
{
    public static IValidatorEntity<Entities.Pedido> Create()
    {
        return new PedidoValidator();
    }
}

public static class PedidoItemValidatorFactory
{
    public static IValidatorEntity<Entities.PedidoItem> Create()
    {
        return new PedidoItemValidator();
    }
}
