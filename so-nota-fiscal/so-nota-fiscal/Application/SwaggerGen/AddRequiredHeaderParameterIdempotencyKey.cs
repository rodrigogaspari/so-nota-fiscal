using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SoNotaFiscal.Application.SwaggerGen
{
    public class AddRequiredHeaderParameterIdempotencyKey : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.CustomAttributes
                .Any(x => x.AttributeType == typeof(IdempotentAPI.Filters.IdempotentAttribute)))
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                var parameterIdempotency =  operation.Parameters.Where(x => x.Name.Equals("idempotencyKey")).FirstOrDefault();
                if (parameterIdempotency == null)
                {
                    operation.Parameters.Add(new OpenApiParameter()
                    {
                        Name = "idempotencyKey",
                        In = ParameterLocation.Header,
                        Schema = new OpenApiSchema() { Type = "String" },
                        Example = new OpenApiString(Guid.NewGuid().ToString())
                    });
                }
                else
                {
                    parameterIdempotency.Example = new OpenApiString(Guid.NewGuid().ToString());
                }
            }
        }
    }
}
