using System;
using System.Collections.Generic;
using System.Text;

namespace JustIoC
{
    /// <summary>
    /// Describes a service registration.
    /// </summary>
    public class JustDescriptor
    {
        /// <summary>
        /// Initializes a new instance of <see cref="JustDescriptor"/> with the given <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <exception cref="ArgumentNullException"><paramref name="serviceType"/> is <c>null</c>.</exception>
        public JustDescriptor(Type serviceType)
        {
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            ImplementationType = ServiceType;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="JustDescriptor"/> with the given <paramref name="serviceType"/>
        /// and <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="implementationType">The <see cref="Type"/> that implements the service.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="serviceType"/> or <paramref name="implementationType"/> is <c>null</c>.
        /// </exception>
        public JustDescriptor(Type serviceType, Type implementationType)
        {
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
        }

        /// <summary>
        /// The <see cref="Type"/> of the service.
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// The <see cref="Type"/> that implements the service.
        /// </summary>
        public Type ImplementationType { get; }
    }
}
