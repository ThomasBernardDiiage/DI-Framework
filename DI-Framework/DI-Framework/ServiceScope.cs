namespace DI_Framework
{
    /// <summary>
    /// Describe a service that can be used to resolve scoped services.
    /// </summary>
    public class ServiceScope
    {
        public DiContainer Container { get; }
        public ServiceScope(DiContainer container)
        {
            Container = container;
        }
    }
}
