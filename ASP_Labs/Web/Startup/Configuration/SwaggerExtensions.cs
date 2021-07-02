using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Web.Startup.Configuration
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP_Labs", Version = "v1" });

                c.CustomSchemaIds(x => x.FullName);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization via Bearer scheme: Bearer {token}",
                    Scheme = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[0]
                    }
                });
                c.DocumentFilter<JsonPatchDocumentFilter>();
            });
        }

        public class JsonPatchDocumentFilter : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                var schemas = swaggerDoc.Components.Schemas.ToList();
                foreach (var item in schemas)
                {
                    if (item.Key.StartsWith("Operation") || item.Key.StartsWith("JsonPatchDocument"))
                        swaggerDoc.Components.Schemas.Remove(item.Key);
                }

                swaggerDoc.Components.Schemas.Add("Operation", new OpenApiSchema
                {
                    Type = "object",
                    Properties = new Dictionary<string, OpenApiSchema>
            {
                {"op", new OpenApiSchema{ Type = "string" } },
                {"value", new OpenApiSchema{ Type = "string"} },
                {"path", new OpenApiSchema{ Type = "string" } }
            }
                });

                swaggerDoc.Components.Schemas.Add("JsonPatchDocument", new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "Operation" }
                    },
                    Description = "Array of operations to perform"
                });

                foreach (var path in swaggerDoc.Paths.SelectMany(p => p.Value.Operations)
                .Where(p => p.Key == Microsoft.OpenApi.Models.OperationType.Patch))
                {
                    foreach (var item in path.Value.RequestBody.Content.Where(c => c.Key != "application/json-patch+json"))
                        path.Value.RequestBody.Content.Remove(item.Key);
                    var response = path.Value.RequestBody.Content.Single(c => c.Key == "application/json-patch+json");
                    response.Value.Schema = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "JsonPatchDocument" }
                    };
                }
            }
        }
    }
}
