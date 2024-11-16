using Refit;

namespace Pediaqui.Payment.Models;

public record CreatePaymentRequest(
    [AliasAs("nroPedido")]
    int numeroPedido,
    [AliasAs("valor")]
    decimal valorPedido
);
