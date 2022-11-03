namespace DI_Framework
{
    /// <summary>
    /// Describes a service with its service type, implementation, and lifetime.
    /// </summary>
    public class ServiceDescriptor
    {
        public Type ServiceType { get; }
        public Type? ImplementationType { get; }

        public object? Implementation { get; internal set; }
        public ServiceLifetime LifeTime { get; }

        public ServiceDescriptor(Type serviceType, ServiceLifetime lifeTime)
        {
            ServiceType = serviceType;
            LifeTime = lifeTime;
        }

        public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifeTime)
        {
            ServiceType = serviceType;
            LifeTime = lifeTime;
            ImplementationType = implementationType;
        }
    }
}
