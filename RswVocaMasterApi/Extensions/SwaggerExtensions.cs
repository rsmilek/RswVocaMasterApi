using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Rsw.VocaMaster.Api.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        const string ApiGithubUrl = "https://github.com/rsmilek/RswVocaMasterApi";
        const string ApiUrl = "https://RswVocaMasterApi.azurewebsites.com";

        services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
        {
            var options = new OpenApiConfigurationOptions()
            {
                Info = new OpenApiInfo()
                {
                    Version = "1.0.0",
                    Title = "Swagger RswVocaMasterApi",
                    Description = $"This is a VocaMaster API designed by [{ApiUrl}]({ApiUrl}).",
                    TermsOfService = new Uri(ApiGithubUrl),
                    Contact = new OpenApiContact()
                    {
                        Name = "Radim Smilek",
                        Email = "rsmilek@seznam.cz",
                        Url = new Uri(ApiGithubUrl),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("http://opensource.org/licenses/MIT"),
                    }
                },
                Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
                OpenApiVersion = OpenApiVersionType.V3,
                IncludeRequestingHostName = true,
                ForceHttps = false,
                ForceHttp = false,
            };

            return options;
        });
    }
}
