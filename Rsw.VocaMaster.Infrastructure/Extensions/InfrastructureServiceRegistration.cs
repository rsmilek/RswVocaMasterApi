using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rsw.VocaMaster.Domain;
using Rsw.VocaMaster.Infrastructure.Configurations;
using Rsw.VocaMaster.SharedKernel.Configurations;

namespace Rsw.VocaMaster.Infrastructure.Extensions;

public static class InfrastructureServiceRegistration
{
    private const string ConnectionString = "ConnectionString";
    private const string DatabaseName = "DatabaseName";

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration[ConnectionString];
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException($""""
                {connectionString} string cannot be null!
                To fix create value '{ConnectionString}=[ADD-THE-CONNECTION-STRING]' in section Values in local.settings.json...
                """");
                
        }

        var databaseName = configuration[DatabaseName];
        if (string.IsNullOrEmpty(databaseName))
        {
            throw new ArgumentException($""""
                {connectionString} string cannot be null!
                To fix create value '{DatabaseName}=[ADD-THE-DATABASE-NAME]' in section Values in local.settings.json...
                """");

        }

        services.AddSingleton<IModelConfiguration, ModelConfiguration>();

        services.AddDbContextFactory<VocaMasterDbContext>(optionsBuilder =>
          optionsBuilder
            .UseCosmos(
              connectionString: connectionString,
              databaseName: databaseName,
              cosmosOptionsAction: options =>
              {
                  options.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Direct);
                  options.MaxRequestsPerTcpConnection(16);
                  options.MaxTcpConnectionsPerEndpoint(32);
              }));

        return services;
    }
}

