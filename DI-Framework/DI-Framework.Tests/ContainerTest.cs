using DI_Framework.Tests.Sut.Services;
using Xunit;

namespace DI_Framework.Tests;

public class ContainerTest
{
    [Fact]
    public void TestResolveUnregisteredService()
    {
        var services = new DiServiceCollection();
            
        var container = services.GenerateContainer(); // Generate container without register our service
        
        // Todo must manage exceptions
        Assert.Throws<Exception>(() => container.GetService<GuidService>()); // Must throw an Exception
    }
}