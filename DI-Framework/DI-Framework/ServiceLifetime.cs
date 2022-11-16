namespace DI_Framework
{
    /// <summary>
    /// LifeTime in DiServiceCollection.
    /// </summary>
    public enum ServiceLifetime
    {
        /// <summary>
        /// Just one instance
        /// </summary>
        Singleton,
        /// <summary>
        /// New instance each time
        /// </summary>
        Transient,
        /// <summary>
        /// New instance for each scope
        /// </summary>
        Scope
    }
}