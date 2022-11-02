using System.Diagnostics;

namespace DI_Framework.Console
{
    public class SomeService : ISomeService
    {
        private readonly IRandomGuidProvider _randomGuidProvider;
        public SomeService(IRandomGuidProvider randomGuidProvider)
        {
            _randomGuidProvider = randomGuidProvider;
        }
        public void PrintSomething()
        {
            Debug.WriteLine(_randomGuidProvider.RandomGuid);
        }
    }
}
