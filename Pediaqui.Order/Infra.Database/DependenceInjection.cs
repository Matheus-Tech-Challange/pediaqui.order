using Domain.Pedido.Ports;
using Infra.Database.Repository.Pedido;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Database;

public static class DependenyInjection
{
    public static IServiceCollection AddInfraData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddDbContext<DatabaseContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Default");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        return services;
    }
}
