using System;
using System.Collections.Generic;
using System.Linq;

namespace JustIoC
{
    public class JustContainer
    {
        private IDictionary<Type, Type> _justServices;
        private IDictionary<Type, object> _justInstances;

        public JustContainer()
        {
            _justServices = new Dictionary<Type, Type>();
            _justInstances = new Dictionary<Type, object>();
        }

        public JustContainer Add<TService>()
        {
            _justServices.Add(typeof(TService), typeof(TService));
            return this;
        }

        public JustContainer Add<TService, TImplementation>()
        {
            _justServices.Add(typeof(TService), typeof(TImplementation));
            return this;
        }

        public TService Get<TService>()
            where TService : class
        {
            return Get(typeof(TService)) as TService;
        }

        private object Get(Type serviceType)
        {
            if (!_justServices.TryGetValue(serviceType, out Type implementationType))
            {
                throw new Exception($"No service could be resolved for type {serviceType}");
            }

            if (!_justInstances.TryGetValue(serviceType, out object instance))
            {
                var constructors = implementationType.GetConstructors();
                if (constructors.Length != 1)
                {
                    throw new Exception($"More than one public constructor found for type '{implementationType}'.");
                }
                var parameters = constructors.Single().GetParameters();
                object[] args = new object[parameters.Length];
                foreach (var param in parameters)
                {
                    var paramInstance = Get(param.ParameterType);
                    args[param.Position] = paramInstance;
                }
                instance = Activator.CreateInstance(implementationType, args);
                _justInstances.Add(serviceType, instance);
                return instance;
            }

            return instance;
        }
    }
}
