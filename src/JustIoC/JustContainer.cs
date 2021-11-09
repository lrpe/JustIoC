using System;
using System.Collections.Generic;

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
            Type serviceType = typeof(TService);

            if (!_justServices.TryGetValue(serviceType, out Type implementationType))
            {
                throw new Exception($"No service could be resolved for type {typeof(TService)}");
            }

            if (!_justInstances.TryGetValue(serviceType, out object instance))
            {
                instance = Activator.CreateInstance(implementationType);
                _justInstances.Add(serviceType, instance);
                return instance as TService;
            }

            return instance as TService;
        }
    }
}
