using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tourist.Application.Commands;
public static class Extensions
{
    public static IServiceBuilder Init(this IServiceCollection services)
    {
        services.AddMemoryCache();
        var builder = ServiceBuilder.Create(services);
        return builder;
    }
    
    public static IServiceBuilder AddCommandHandlers(this IServiceBuilder serviceBuilder)
    {
        serviceBuilder.Services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return serviceBuilder;
    }

    public static IServiceBuilder AddInMemoryCommandDispatcher(this IServiceBuilder serviceBuilder)
    {
        serviceBuilder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        return serviceBuilder;
    }
}
