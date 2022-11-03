namespace DI_Framework.Tests.Sut.Interfaces;

public interface IGuidService
{
    Guid Generate();
    bool IsEqual(Guid firstGuid, Guid secondGuid);
}