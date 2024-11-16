using Domain.Enuns;

namespace Domain.Pedido.Extensions;

public static class StatusPedidoExtension
{
    public static string ToText(this StatusPedido status)
    {
        switch (status)
        {
            case StatusPedido.INICIADO:
                return nameof(StatusPedido.INICIADO);
            case StatusPedido.CANCELADO:
                return nameof(StatusPedido.CANCELADO);
            case StatusPedido.RECEBIDO:
                return nameof(StatusPedido.RECEBIDO);
            case StatusPedido.PENDENTE_PAGAMENTO:
                return nameof(StatusPedido.PENDENTE_PAGAMENTO);
            case StatusPedido.EM_PREPARACAO:
                return nameof(StatusPedido.EM_PREPARACAO);
            case StatusPedido.PRONTO:
                return nameof(StatusPedido.PRONTO);
            case StatusPedido.FINALIZADO:
                return nameof(StatusPedido.FINALIZADO);
            default:
                throw new ArgumentException($"Status '{status}' não existe!");
        }
    }
};
