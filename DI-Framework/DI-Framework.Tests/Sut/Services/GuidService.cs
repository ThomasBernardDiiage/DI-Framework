using DI_Framework.Tests.Sut.Interfaces;

namespace DI_Framework.Tests.Sut.Services;

public class GuidService : IGuidService
{
    public Guid Generate() => Guid.NewGuid();

    public bool IsEqual(Guid firstGuid, Guid secondGuid) =>  firstGuid == secondGuid;
}