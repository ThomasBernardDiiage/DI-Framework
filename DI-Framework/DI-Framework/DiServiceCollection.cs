using DI_Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;

namespace DI_Framework
{
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

        private void RegisterSingleton(Type service)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(service, ServiceLifetime.Singleton));
        }
        private void RegisterSingleton(Type service, Type implementation)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(service, implementation, ServiceLifetime.Singleton));
        }

        /// <summary>
        ///  Adds a transient service of the type specified in <typeparamref name="TService"/>
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        public void RegisterTransient<TService>() where TService : class
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

        //https://www.crispy-engineering.com/registering-all-types-as-generic-interfaces-in-assembly-in-dotnet-core/
        private void RegisterTransient(Type service)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(service, ServiceLifetime.Transient));
        }
        private void RegisterTransient(Type service, Type implementation)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(service, implementation, ServiceLifetime.Transient));
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

        private void RegisterScope(Type service)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(service, ServiceLifetime.Scope));
        }
        private void RegisterScope(Type service, Type implementation)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(service, implementation, ServiceLifetime.Scope));
        }

        /// <summary>
        /// Build a container
        /// </summary>
        /// <returns></returns>
        public DiContainer GenerateContainer()
        {
            return new DiContainer(_serviceDescriptors);
        }
        public void RegisterFromAssembly(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException(nameof(text));  
            }

            var registrations = JsonConvert.DeserializeObject<IEnumerable<Registration>>(text, new StringEnumConverter());

            foreach (var registration in registrations)
            {
                var serviceType = Assembly.GetCallingAssembly().GetTypes()
                                           .FirstOrDefault(e => e.Name == registration.InterfaceName);

                if (!string.IsNullOrEmpty(registration.InterfaceName) && serviceType is null)
                {
                    throw new Exception("Couldn't reach the interface");
                }
               
                var implementationType = Assembly.GetCallingAssembly().GetTypes()
                                            .FirstOrDefault(e => e.Name == registration.ClasseName);

                if (!string.IsNullOrEmpty(registration.ClasseName) && implementationType is null)
                {
                    throw new Exception("Couldn't reach the class");
                }

                if(implementationType is null && serviceType is not null)
                {
                    throw new Exception("You can't register an interface without its implementation");
                }

                switch (registration.ServiceLifetime)
                {
                    case ServiceLifetime.Transient:
                        if (serviceType is not null && implementationType is not null)
                        {
                            RegisterTransient(serviceType, implementationType);
                        }
                        else if (implementationType is not null && serviceType is null)
                        {
                            RegisterTransient(implementationType);
                        }
                        break;
                    case ServiceLifetime.Singleton:
                        if (serviceType is not null && implementationType is not null)
                        {
                            RegisterSingleton(serviceType, implementationType);
                        }
                        else if (implementationType is not null && serviceType is null)
                        {
                            RegisterSingleton(implementationType);
                        }
                        break;
                    case ServiceLifetime.Scope:
                        if (serviceType is not null && implementationType is not null)
                        {
                            RegisterScope(serviceType, implementationType);
                        }
                        else if (implementationType is not null && serviceType is null)
                        {
                            RegisterScope(implementationType);                         
                        }
                        break;
                    default:
                        throw new NotImplementedException("That service lifetime is not implemented");
                }
            }
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

                if (serviceDescriptor.LifeTime == ServiceLifetime.Singleton)
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
}