using Pediaqui.Payment.Models;
using Refit;

namespace Pediaqui.Payment.Ports;

public interface IPaymentRepository
{
    [Post("/api/payment/create")]
    Task<string> CreatePayment(CreatePaymentRequest paymentRequest);

    [Get("/api/payment/check-status/{nroPedido}")]
    Task<string> GetPaymentStatus([AliasAs("nroPedido")] int numeroPedido);
}
