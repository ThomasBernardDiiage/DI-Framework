using DI_Framework;
using DI_Framework.Exceptions;

public class DiContainer
{
    private List<ServiceDescriptor> _serviceDescriptors = new List<ServiceDescriptor>();

    public DiContainer(List<ServiceDescriptor> serviceDescriptors)
    {
        _serviceDescriptors = serviceDescriptors;
    }

    public object GetService(Type serviceType)
    {
        if(serviceType is null)
        {
            throw new ArgumentNullException();
        }

        var descriptor = _serviceDescriptors
            .SingleOrDefault(x => x.ServiceType == serviceType);

        if (descriptor is null)
        {
            throw new NotRegisteredException($"Service of type {serviceType.Name} isn't registered");
        }

        if (descriptor.Implementation is not null)
        {
            return descriptor.Implementation;
        }

        var actualType = descriptor.ImplementationType ?? descriptor.ServiceType;

        if (actualType.IsAbstract || actualType.IsInterface)
        {
            throw new AbstractOrInterfaceClassException("Cannot instantiate abstract classes or interfaces");
        }

        var constructorInfo = actualType
            .GetConstructors()
            .MaxBy(x => x.GetParameters().Count());

        var parameters = constructorInfo!.GetParameters()
            .Select(x => GetService(x.ParameterType))
            .ToArray();

        var implementation = Activator.CreateInstance(actualType, parameters);

        if (descriptor.LifeTime == ServiceLifetime.Singleton)
        {
            descriptor.Implementation = implementation!;
        }

        return implementation!;
    }

    public T GetService<T>()
    {
        return (T)GetService(typeof(T));
    }
}