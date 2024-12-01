using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pediaqui.Payment.Ports;
using Refit;

namespace Pediaqui.Payment;

public record PaymentService(string URL, int port);

public static class DependenceInjection
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services, IConfiguration configuration)
    {
        PaymentService service = configuration.GetSection(nameof(PaymentService)).Get<PaymentService>()!;

        if (service is null) throw new ArgumentNullException(nameof(PaymentService));

        services
            .AddRefitClient<IPaymentRepository>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{service.URL}:{service.port}"));

        return services;
    }
}
