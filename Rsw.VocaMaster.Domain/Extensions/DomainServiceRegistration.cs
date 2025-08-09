using Microsoft.Extensions.DependencyInjection;

namespace Rsw.VocaMaster.Domain.Extensions;

public static class DomainServiceRegistration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        // TODO: services.AddScoped<IXxxService, XxxService>();

        return services;
    }
}
