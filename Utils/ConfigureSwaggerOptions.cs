using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RestApi.Utils
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "My Api resftul",
                Version = description.ApiVersion.ToString(),
                Description = "Este es mi primer versionado de Api",
                Contact = new OpenApiContact()
                {
                    Email = "mathiasdvt7@gmail.com",
                    Name = "Mathias"
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += "Esta versión de Api está en desuso";
            }

            return info;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

    }
}
