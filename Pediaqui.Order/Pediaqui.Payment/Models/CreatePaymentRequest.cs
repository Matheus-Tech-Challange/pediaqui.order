using Refit;

namespace Pediaqui.Payment.Models;

public record CreatePaymentRequest(
    int NumeroPedido,
    decimal Valor
);
