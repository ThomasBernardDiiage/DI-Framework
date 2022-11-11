using DI_Framework.Tests.Sut.Interfaces;

namespace DI_Framework.Tests.Sut.Services
{
    internal class RandomGuidProvider : IRandomGuidProvider
    {
        public Guid RandomGuid { get; } = Guid.NewGuid();
    }
}
