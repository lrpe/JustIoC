using System;
using System.Collections.Generic;
using System.Linq;

namespace JustIoC
{
    /// <summary>
    /// Provides a collection of services for dependency injection.
    /// </summary>
    public class JustContainer
    {
        private readonly IDictionary<Type, JustDescriptor> _justServices;
        private readonly IDictionary<Type, object> _justInstances;

        /// <summary>
        /// Initializes a new instance of the <see cref="JustContainer"/> class.
        /// </summary>
        public JustContainer()
        {
            _justServices = new Dictionary<Type, JustDescriptor>();
            _justInstances = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Registers the given service of type <typeparamref name="TService"/> with the given
        /// <paramref name="lifetime"/>.
        /// </summary>
        /// <param name="lifetime">Specifies the lifetime of the service.</param>
        /// <typeparam name="TService">The type of service to register.</typeparam>
        /// <returns>The same <see cref="JustContainer"/> reference, so that multiple calls can be chained.</returns>
        /// <exception cref="JustException">Service is already registered.</exception>
        public JustContainer Add<TService>(ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TService : class
        {
            var descriptor = new JustDescriptor(typeof(TService), typeof(TService), lifetime);
            if (!_justServices.TryAdd(descriptor.ServiceType, descriptor))
            {
                throw new JustException($"Service {descriptor.ServiceType} is already registered.");
            }
            return this;
        }

        /// <summary>
        /// Registers the given service of type <typeparamref name="TService"/> with the implementation type
        /// <typeparamref name="TImplementation"/> with the given <paramref name="lifetime"/>.
        /// </summary>
        /// <param name="lifetime">Specifies the lifetime of the service.</param>
        /// <typeparam name="TService">The type of service to register.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementing class of the service.</typeparam>
        /// <returns>The same <see cref="JustContainer"/> reference, so that multiple calls can be chained.</returns>
        /// <exception cref="JustException">Service is already registered.</exception>
        public JustContainer Add<TService, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TService : class
            where TImplementation : class, TService
        {
            var descriptor = new JustDescriptor(typeof(TService), typeof(TImplementation), lifetime);
            if (!_justServices.TryAdd(descriptor.ServiceType, descriptor))
            {
                throw new JustException($"Service {descriptor.ServiceType} is already registered.");
            }
            return this;
        }

        /// <summary>
        /// Registers the service described by the given <paramref name="serviceDescriptor"/>.
        /// </summary>
        /// <param name="serviceDescriptor">The <see cref="JustContainer"/> that describes the service.</param>
        /// <returns>The same <see cref="JustContainer"/> reference, so that multiple calls can be chained.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="serviceDescriptor"/> argument is <c>null</c>.
        /// </exception>
        /// <exception cref="JustException">Service is already registered.</exception>
        public JustContainer Add(JustDescriptor serviceDescriptor)
        {
            if (serviceDescriptor is null)
            {
                throw new ArgumentNullException(nameof(serviceDescriptor));
            }

            if (!_justServices.TryAdd(serviceDescriptor.ServiceType, serviceDescriptor))
            {
                throw new JustException($"Service {serviceDescriptor.ServiceType} is already registered.");
            }

            return this;
        }

        /// <summary>
        /// Get a service of type <typeparamref name="TService"/> from the container.
        /// </summary>
        /// <typeparam name="TService">The type of service to get.</typeparam>
        /// <returns>A service of type <typeparamref name="TService"/>.</returns>
        /// <exception cref="JustException">Service could not be resolved, or no suitable constructor.</exception>
        public TService Get<TService>()
            where TService : class
        {
            return Get(typeof(TService)) as TService;
        }

        private object Get(Type serviceType)
        {
            if (!_justServices.TryGetValue(serviceType, out JustDescriptor descriptor))
            {
                throw new JustException($"No service could be resolved for type {serviceType}");
            }

            if (!_justInstances.TryGetValue(serviceType, out object instance))
            {
                var constructors = descriptor.ImplementationType.GetConstructors();
                if (constructors.Length != 1)
                {
                    throw new JustException($"More than one public constructor found for type '{descriptor.ImplementationType}'.");
                }
                var parameters = constructors.Single().GetParameters();
                object[] args = new object[parameters.Length];
                foreach (var param in parameters)
                {
                    var paramInstance = Get(param.ParameterType);
                    args[param.Position] = paramInstance;
                }
                instance = Activator.CreateInstance(descriptor.ImplementationType, args);
                _justInstances.Add(serviceType, instance);
                return instance;
            }

            return instance;
        }
    }
}
