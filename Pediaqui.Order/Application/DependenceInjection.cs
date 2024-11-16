using Application.Presenters;
using Domain.Pedido.Ports;
using Microsoft.Extensions.DependencyInjection;
using Pediaqui.Cliente.Ports;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddScoped<NotificationContext>();
        services.AddScoped<PedidoPresenter>();

        return services;
    }
}
