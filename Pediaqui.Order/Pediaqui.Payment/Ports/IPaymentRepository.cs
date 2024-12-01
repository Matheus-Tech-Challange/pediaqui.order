using Pediaqui.Payment.Models;
using Refit;

namespace Pediaqui.Payment.Ports;

public interface IPaymentRepository
{
    [Post("/api/payment")]
    Task<string> CreatePayment(CreatePaymentRequest paymentRequest);

    [Get("/api/payment/{id}")]
    Task<string> GetPaymentStatus([AliasAs("id")] int id);
}
