using DI_Framework;

/// <summary>
/// Describes a service for a collection of service descriptors
/// </summary>
public class DiServiceCollection
{
    private List<ServiceDescriptor> _serviceDescriptors = new List<ServiceDescriptor>();

    /// <summary>
    ///  Adds a singleton service of the type specified in <typeparamref name="TService"/>
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public void RegisterSingleton<TService>()
    {
        _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton));
    }

    /// <summary>
    ///  Adds a singleton service of the type specified in <typeparamref name="TService"/> with an
    ///  implementation type specified in <typeparamref name="TImplementation"/> to the
    ///  specified <see cref="DiServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    public void RegisterSingleton<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton));
    }

    /// <summary>
    ///  Adds a transient service of the type specified in <typeparamref name="TService"/>
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public void RegisterTransient<TService>()
    {
        _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient));
    }

    /// <summary>
    ///  Adds a transient service of the type specified in <typeparamref name="TService"/> with an
    ///  implementation type specified in <typeparamref name="TImplementation"/> to the
    ///  specified <see cref="DiServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    public void RegisterTransient<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient));
    }

    /// <summary>
    /// Build a container
    /// </summary>
    /// <returns></returns>
    public DiContainer GenerateContainer()
    {
        return new DiContainer(_serviceDescriptors);
    }
}