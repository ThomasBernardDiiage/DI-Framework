namespace DI_Framework
{
    public class ServiceDescriptor
    {
        public Type? ServiceType { get; }
        public Type? ImplementationType { get; }

        public object? Implementation { get; internal set; }
        public ServiceLifetime LifeTime { get; }

        public ServiceDescriptor(object implementation, ServiceLifetime lifeTime)
        {
            ServiceType = implementation.GetType();
            Implementation = implementation!;
            LifeTime = lifeTime;
        }

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
