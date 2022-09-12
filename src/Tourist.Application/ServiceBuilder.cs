using System;
using System.Collections.Generic;
using Tourist.Application.Initializers;
using Microsoft.Extensions.DependencyInjection;

namespace Tourist.Application;
public sealed class ServiceBuilder : IServiceBuilder
{
    private readonly IServiceCollection _services;

    IServiceCollection IServiceBuilder.Services => _services;

    private readonly List<Action<IServiceProvider>> _buildActions;

    private ServiceBuilder(IServiceCollection services)
    {
        _services = services;
        _buildActions = new List<Action<IServiceProvider>>();
        _services.AddSingleton<IStartupInitializer>(new StartupInitializer());
    }
    public static IServiceBuilder Create(IServiceCollection services)
        => new ServiceBuilder(services);

    public IServiceProvider Build()
    {
        var serviceProvider = _services.BuildServiceProvider();
        _buildActions.ForEach(a => a(serviceProvider));
        return serviceProvider;
    }

    public void AddBuildAction(Action<IServiceProvider> execute)
        => _buildActions.Add(execute);

    public void AddInitializer(IInitializer initializer)
        => AddBuildAction(sp =>
        {
            var startupInitializer = sp.GetService<IStartupInitializer>();
            startupInitializer.AddInitializer(initializer);
        });

    public void AddInitializer<TInitializer>() where TInitializer : IInitializer
        => AddBuildAction(sp =>
        {
            var initializer = sp.GetService<TInitializer>();
            var startupInitializer = sp.GetService<IStartupInitializer>();
            startupInitializer.AddInitializer(initializer);
        });
}