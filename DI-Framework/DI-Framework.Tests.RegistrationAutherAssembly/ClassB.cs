using DI_Framework.Tests.Sut.Interfaces;

namespace DI_Framework.Tests.Sut.Services
{
    internal class ClassB : IClassB
    {
        public Guid RandomGuid { get; } = Guid.NewGuid();
    }
}
