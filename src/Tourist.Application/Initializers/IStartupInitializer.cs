namespace Tourist.Application.Initializers;
public interface IStartupInitializer : IInitializer
{
    void AddInitializer(IInitializer initializer);
}
