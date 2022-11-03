using DI_Framework.Tests.Sut.Interfaces;
using DI_Framework.Tests.Sut.Services;

namespace DI_Framework.Tests;

public class UnitTest
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
    public void TestSingletonInstanceInterface()
    {
        var services = new DiServiceCollection();
        
        services.RegisterSingleton<GuidService>(); // Register a singleton
        var container = services.GenerateContainer();

        var sut1 = container.GetService<GuidService>();
        var sut2 = container.GetService<GuidService>();
        
        Assert.Equal(sut1, sut2); // Our Service must be the same instance
    }
    
    
    [Fact]
    public void TestTransient()
    {
        
    }
    
    
    [Fact]
    public void TestScope()
    {
        
    }

    
    
}
