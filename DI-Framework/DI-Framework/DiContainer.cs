using DI_Framework;

/// <summary>
/// Describes a service to retrieve a collection of service descriptors
/// </summary>
public class DiContainer
{
    private List<ServiceDescriptor> _serviceDescriptors = new();

    public DiContainer(List<ServiceDescriptor> serviceDescriptors)
    {
        _serviceDescriptors = serviceDescriptors;
    }

    /// <summary>
    /// Get service of type <paramref name="serviceType"/>from the <see cref="DiContainer"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public object GetService(Type serviceType)
    {
        if (serviceType is null)
        {
            throw new ArgumentNullException();
        }

        var descriptor = _serviceDescriptors
            .SingleOrDefault(x => x.ServiceType == serviceType);

        if (descriptor is null)
        {
            throw new Exception($"Service of type {serviceType.Name} isn't registered");
        }

        if (descriptor.Implementation is not null)
        {
            return descriptor.Implementation;
        }

        Type actualType = descriptor.ImplementationType ?? descriptor.ServiceType;

        if (actualType is null || actualType.IsAbstract || actualType.IsInterface)
        {
            throw new Exception("Cannot instantiate abstract classes or interfaces or nulles classe");
        }

        var moreThanOneConstructor = actualType
            .GetConstructors()
            .Count() > 1 ? true : false;

        if (moreThanOneConstructor)
        {
            throw new Exception("You must have only one constructor");
        }

        var constructorInfo = actualType
            .GetConstructors()
            .First();

        var parameters = constructorInfo.GetParameters()
            .Select(x => GetService(x.ParameterType))
            .ToArray();

        var implementation = Activator.CreateInstance(actualType, parameters);

        if (descriptor.LifeTime == ServiceLifetime.Singleton)
        {
            descriptor.Implementation = implementation!;
        }

        return implementation!;
    }

    /// <summary>
    /// Get service of type <typeparamref name="T"/> from the <see cref="DiContainer"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetService<T>()
        where T : notnull
    {
        return (T)GetService(typeof(T));
    }
}