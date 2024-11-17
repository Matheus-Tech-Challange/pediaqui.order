using Application.Presenters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pediaqui.Catalog;
using Pediaqui.Payment;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddScoped<NotificationContext>();
        services.AddScoped<PedidoPresenter>();

        services.AddCatalogService(configuration);
        services.AddPaymentService(configuration);

        return services;
    }
}
