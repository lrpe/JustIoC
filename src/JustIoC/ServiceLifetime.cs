namespace JustIoC
{
    /// <summary>
    /// Specifies the lifetime of a service registration in the <see cref="JustContainer"/>.
    /// </summary>
    public enum ServiceLifetime
    {
        /// <summary>
        /// Specifies that a single instance of the service will be created.
        /// </summary>
        Singleton,

        /// <summary>
        /// Specifies that a new instance of the service will be created each time it is requested.
        /// </summary>
        Transient,
    }
}
