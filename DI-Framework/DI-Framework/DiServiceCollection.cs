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
    ///  Adds a scope service of the type specified in <typeparamref name="TService"/>
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public void RegisterScope<TService>()
    {
        _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), ServiceLifetime.Scope));
    }

    /// <summary>
    ///  Adds a scope service of the type specified in <typeparamref name="TService"/> with an
    ///  implementation type specified in <typeparamref name="TImplementation"/> to the
    ///  specified <see cref="DiServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    public void RegisterScope<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Scope));
    }

    /// <summary>
    /// Build a container
    /// </summary>
    /// <returns></returns>
    public DiContainer GenerateContainer()
    {
        return new DiContainer(_serviceDescriptors);
    }

    /// <summary>
    /// Builld a scope
    /// </summary>
    /// <returns></returns>
    public ServiceScope CreateScope()
    {
        var servicesDescriptors = GetServicesWithoutScopeInstances();
        return new ServiceScope(new DiContainer(servicesDescriptors));
    }

    private List<ServiceDescriptor> GetServicesWithoutScopeInstances()
    {
        var copyServiceDescriptors = new List<ServiceDescriptor>(_serviceDescriptors.Count);

        for (int i = 0; i < _serviceDescriptors.Count; i++)
        {
            var serviceDescriptor = _serviceDescriptors[i];

            if(serviceDescriptor.LifeTime == ServiceLifetime.Singleton)
            {
                copyServiceDescriptors.Add(serviceDescriptor);
                continue;
            }

            var newServiceDescriptor = new ServiceDescriptor(
                serviceDescriptor.ServiceType,
                serviceDescriptor.ImplementationType,
                serviceDescriptor.LifeTime);


            copyServiceDescriptors.Add(newServiceDescriptor);
        }

        return copyServiceDescriptors;
    }
}