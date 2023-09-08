using Application.AutoMapper;
using Application.Models.Requests;
using Application.Services;
using Application.Services.Interfaces;
using Application.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddLogging();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IValidator<ClientRequest>, ClientValidator>();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            return services;
        }
    }
}
