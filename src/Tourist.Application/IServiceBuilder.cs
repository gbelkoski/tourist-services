using Tourist.Application.Initializers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tourist.Application
{
    public interface IServiceBuilder
    {
        IServiceCollection Services { get; }       
        IServiceProvider Build();
        void AddBuildAction(Action<IServiceProvider> execute);
        void AddInitializer(IInitializer initializer);
        void AddInitializer<TInitializer>() where TInitializer : IInitializer;
    }
}
