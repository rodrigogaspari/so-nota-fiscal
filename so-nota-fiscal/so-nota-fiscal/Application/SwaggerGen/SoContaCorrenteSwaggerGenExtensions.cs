using Microsoft.OpenApi.Models;
using System.Reflection;

namespace SoContaCorrente.Application.SwaggerGen
{
    public static class SoContaCorrenteSwaggerGenExtensions
    {
        public static IServiceCollection AddSwaggerSoContaCorrenteCustomizations(this IServiceCollection services)
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
                    Title = "Smart Online - Conta Corrente Baking API",
                    Description = "Uma Web API ASP.NET Core para apresentação",
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }

        public static WebApplication AddSwaggerSoContaCorrenteCustomizations(this WebApplication app) 
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
