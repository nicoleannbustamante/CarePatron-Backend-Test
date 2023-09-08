using Infrastructure.EFCore.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // ioc
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "Test"));

            services.AddScoped<DataSeeder>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();

            return services;
        }
    }
}
