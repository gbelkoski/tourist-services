using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tourist.Application.Queries;
public static class Extensions
{
    public static IServiceBuilder AddQueryHandlers(this IServiceBuilder serviceBuilder)
    {
        serviceBuilder.Services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return serviceBuilder;
    }

    public static IServiceBuilder AddInMemoryQueryDispatcher(this IServiceBuilder serviceBuilder)
    {
        serviceBuilder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        return serviceBuilder;
    }
}
