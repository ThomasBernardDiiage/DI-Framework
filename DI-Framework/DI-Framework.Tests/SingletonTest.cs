using DI_Framework.Tests.Sut.Interfaces;
using DI_Framework.Tests.Sut.Services;
using Xunit;

namespace DI_Framework.Tests;

public class SingletonTest
{
    [Fact]
    public void TestSingletonInstanceInterfaceAndImplementation()
    {
        var services = new DiServiceCollection();
        
        services.RegisterSingleton<IGuidService, GuidService>(); // Register a singleton
        var container = services.GenerateContainer();
        
        var sut1 = container.GetService<IGuidService>();
        var sut2 = container.GetService<IGuidService>();
        
        Assert.Equal(sut1, sut2); // Our Service must be the same instance
    }
    
    [Fact]
    public void TestSingletonInstanceImplementation()
    {
        var services = new DiServiceCollection();
        
        services.RegisterSingleton<GuidService>(); // Register a singleton
        var container = services.GenerateContainer();

        var sut1 = container.GetService<GuidService>();
        var sut2 = container.GetService<GuidService>();
        
        Assert.Equal(sut1, sut2); // Our Service must be the same instance
    }
}