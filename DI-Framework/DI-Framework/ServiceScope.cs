namespace DI_Framework
{
    /// <summary>
    /// Describe a service that can be used to resolve scoped services.
    /// </summary>
    public class ServiceScope : IDisposable
    {
        private bool _disposed;
        public DiContainer Container { get; }
        public ServiceScope(DiContainer container)
        {
            Container = container;
        }

        public void Dispose()
        {

            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }
    }
}
