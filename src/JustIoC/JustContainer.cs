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
        private IDictionary<Type, JustDescriptor> _justServices;
        private IDictionary<Type, object> _justInstances;

        /// <summary>
        /// Initializes a new instance of the <see cref="JustContainer"/> class.
        /// </summary>
        public JustContainer()
        {
            _justServices = new Dictionary<Type, JustDescriptor>();
            _justInstances = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Registers the given service of type <typeparamref name="TService"/> as a singleton.
        /// </summary>
        /// <typeparam name="TService">The type of service to register.</typeparam>
        /// <returns>The same <see cref="JustContainer"/> reference, so that multiple calls can be chained.</returns>
        public JustContainer Add<TService>()
        {
            var descriptor = new JustDescriptor(typeof(TService), typeof(TService));
            _justServices.Add(descriptor.ServiceType, descriptor);
            return this;
        }

        /// <summary>
        /// Registers the given service of type <typeparamref name="TService"/> with the implementation type
        /// <typeparamref name="TImplementation"/> as a singleton.
        /// </summary>
        /// <typeparam name="TService">The type of service to register.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementing class of the service.</typeparam>
        /// <returns>The same <see cref="JustContainer"/> reference, so that multiple calls can be chained.</returns>
        public JustContainer Add<TService, TImplementation>()
        {
            var descriptor = new JustDescriptor(typeof(TService), typeof(TImplementation));
            _justServices.Add(descriptor.ServiceType, descriptor);
            return this;
        }

        /// <summary>
        /// Get a service of type <typeparamref name="TService"/> from the container.
        /// </summary>
        /// <typeparam name="TService">The type of service to get.</typeparam>
        /// <returns>A service of type <typeparamref name="TService"/>.</returns>
        public TService Get<TService>()
            where TService : class
        {
            return Get(typeof(TService)) as TService;
        }

        private object Get(Type serviceType)
        {
            if (!_justServices.TryGetValue(serviceType, out JustDescriptor descriptor))
            {
                throw new Exception($"No service could be resolved for type {serviceType}");
            }

            if (!_justInstances.TryGetValue(serviceType, out object instance))
            {
                var constructors = descriptor.ImplementationType.GetConstructors();
                if (constructors.Length != 1)
                {
                    throw new Exception($"More than one public constructor found for type '{descriptor.ImplementationType}'.");
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
