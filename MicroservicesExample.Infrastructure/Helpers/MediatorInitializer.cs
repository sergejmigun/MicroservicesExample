using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MicroservicesExample.Infrastructure.Helpers
{
    public static class MediatorInitializer
    {
        public static void Init(IServiceCollection services, Assembly assembly)
        {
            services.AddScoped<ServiceFactory>(context =>
            {
                return t => context.GetService(t);
            });

            AddMediatorHandlers(services, assembly);

            services.AddScoped<IMediator, Mediator>();
        }

        private static IServiceCollection AddMediatorHandlers(IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                {
                    services.AddTransient(handlerType.AsType(), type.AsType());
                }
            }

            return services;
        }
    }
}
