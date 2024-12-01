using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pediaqui.Catalog.Ports;
using Refit;

namespace Pediaqui.Catalog;

public record CatalogService(string URL, int port);

public static class DependenceInjection
{
    public static IServiceCollection AddCatalogService(this IServiceCollection services, IConfiguration configuration)
    {
        CatalogService service = configuration.GetSection(nameof(CatalogService)).Get<CatalogService>()!;

        if (service is null) throw new ArgumentNullException(nameof(CatalogService));

        services
            .AddRefitClient<ICatalogRepository>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{service.URL}:{service.port}"));

        return services;
    }
}
