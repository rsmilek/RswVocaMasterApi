using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rsw.VocaMaster.Api.Extensions;
using Rsw.VocaMaster.Domain.Extensions;
using Rsw.VocaMaster.Infrastructure.Extensions;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights()
    .AddSwagger()
    .AddInfrastructure(builder.Configuration)
    .AddDomain()
    ;

builder.Build().Run();
