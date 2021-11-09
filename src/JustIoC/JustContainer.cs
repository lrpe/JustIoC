using System;
using System.Collections.Generic;

namespace JustIoC
{
    public class JustContainer
    {
        private IDictionary<Type, Type> _justServices;

        public JustContainer()
        {
            _justServices = new Dictionary<Type, Type>();
        }

        public JustContainer Add<TService>()
        {
            _justServices.Add(typeof(TService), typeof(TService));
            return this;
        }

        public TService Get<TService>()
            where TService : class
        {
            if (_justServices.TryGetValue(typeof(TService), out Type implementation))
            {
                return Activator.CreateInstance(implementation) as TService;
            }
            throw new Exception($"No service could be resolved for type {typeof(TService)}");
        }
    }
}
