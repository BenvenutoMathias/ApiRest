using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RestApi.DataAccess;
using RestApi.Services;
using RestApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi
{
    public class Startup
    {
        private RequestLocalizationOptions localizationOptions;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this.localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(suportedCultures[0])
                .AddSupportedCultures(suportedCultures)
                .AddSupportedUICultures(suportedCultures);
        }

        private string[] suportedCultures = new[] { "en-US", "es-ES", "fr-FR", "de-DE" };

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("UniversityDB");
            
            //3. Add context
            services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

            //7. Add Service of Jwt Autorization
            //To do
            services.AddJwtTokenServices(Configuration);
            // 1. Localization
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllers();

            //4. Add Custom Services (folder services)
            services.AddScoped<IGeneralServices, GeneralServices>();

            //8. Add Authorization (claims) [UserOnly]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
            });

            services.AddControllers();
            
            services.AddHttpClient();
            
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });
            
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;

            });
            //To Do: Add the rest of services
            services.AddSwaggerGen(o =>
            {
                //o.SwaggerDoc("v1", new OpenApiInfo { Title = "RestApi", Version = "v1" });

                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization Header using Bearer Scheme"
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            // 2. Supported Cultures
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(suportedCultures[0])
                .AddSupportedCultures(suportedCultures)
                .AddSupportedUICultures(suportedCultures);

            //5. CORS Configuration
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            // 3. Add localization to app
            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant()
                        );
                    }
                });
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors("CorsPolicy");
        }
    }
}
