using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LingoLabs.Application
{
    public static class ApplicationServiceRegistrationDI
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

        }
    }
}
