namespace DI_Framework
{
    /// <summary>
    /// Spécifie la durée de vie d’un service dans un DiServiceCollection.
    /// </summary>
    public enum ServiceLifetime
    {
        /// <summary>
        /// Spécifie qu’une seule instance du service sera créée.
        /// </summary>
        Singleton,
        /// <summary>
        /// Spécifie qu’une nouvelle instance du service sera créée chaque fois qu’une demande est effectuée.
        /// </summary>
        Transient,
        /// <summary>
        /// Spécifie qu’une nouvelle instance du service sera créée pour chaque étendue.
        /// </summary>
        Scope
    }
}