﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AduanasTec.Swagger
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType?
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Any() == true
                || context.MethodInfo
                    .GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>()
                    .Any();

            if (hasAuthorize)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
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
                            new List<string>()
                        }
                    }
                };
            }
        }
    }
}
