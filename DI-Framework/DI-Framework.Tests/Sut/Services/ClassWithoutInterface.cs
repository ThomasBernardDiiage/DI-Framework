using DI_Framework.Tests.Sut.Interfaces;

namespace DI_Framework.Tests.Sut.Services
{
    internal class ClassWithoutInterface
    {
        private readonly IRandomGuidProvider _randomGuidProvider;

        public ClassWithoutInterface(IRandomGuidProvider randomGuidProvider)
        {
            _randomGuidProvider = randomGuidProvider;
        }

        public Guid GetGuid()
        {
            return _randomGuidProvider.RandomGuid;
        }
    }
}
