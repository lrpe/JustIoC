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
        /// <remarks>
        /// <paramref name="serviceType"/> must have only one public constructor, otherwise the container will not
        /// be able to create new instances.
        /// </remarks>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="lifetime">Specifies the lifetime of the service.</param>
        /// <exception cref="ArgumentNullException"><paramref name="serviceType"/> is <c>null</c>.</exception>
        public JustDescriptor(Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            ImplementationType = ServiceType;
            Lifetime = lifetime;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="JustDescriptor"/> with the given <paramref name="serviceType"/>
        /// and <paramref name="implementationType"/>.
        /// </summary>
        /// <remarks>
        /// <paramref name="implementationType"/> must have only one public constructor, otherwise the container will
        /// not be able to create new instances.
        /// </remarks>
        /// <param name="serviceType">The <see cref="Type"/> of the service.</param>
        /// <param name="implementationType">The <see cref="Type"/> that implements the service.</param>
        /// <param name="lifetime">Specifies the lifetime of the service.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="serviceType"/> or <paramref name="implementationType"/> is <c>null</c>.
        /// </exception>
        public JustDescriptor(
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            ServiceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
            Lifetime = lifetime;
        }

        /// <summary>
        /// The <see cref="Type"/> of the service.
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// The <see cref="Type"/> that implements the service.
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// Gets the lifetime of the service.
        /// </summary>
        public ServiceLifetime Lifetime { get; }
    }
}
