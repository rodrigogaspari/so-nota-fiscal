using Microsoft.OpenApi.Models;
using System.Reflection;

namespace SoNotaFiscal.Application.SwaggerGen
{
    public static class SoNotaFiscalSwaggerGenExtensions
    {
        public static IServiceCollection AddSwaggerSoNotaFiscalCustomizations(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AddRequiredHeaderParameterIdempotencyKey>();
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Smart Online - Nota Fiscal Emissor API",
                    Description = "Uma Web API ASP.NET Core para apresentação",
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }

        public static WebApplication AddSwaggerSoNotaFiscalCustomizations(this WebApplication app) 
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });
            }

            return app;
        } 
    }
}
