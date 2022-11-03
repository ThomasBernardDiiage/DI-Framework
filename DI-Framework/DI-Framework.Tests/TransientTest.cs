using DI_Framework.Tests.Sut.Interfaces;
using DI_Framework.Tests.Sut.Services;
using Xunit;

namespace DI_Framework.Tests;

public class TransientTest
{
    [Fact]
    public void TestTransientInstanceInterfaceAndImplementation()
    {
        var services = new DiServiceCollection();
        
        services.RegisterTransient<IGuidService, GuidService>(); // Register a transient service with an interface
        var container = services.GenerateContainer();
        
        var sut1 = container.GetService<IGuidService>();
        var sut2 = container.GetService<IGuidService>();
        
        Assert.NotEqual(sut1, sut2); // Our Service must be a different instance
    }
    
    [Fact]
    public void TestTransientInstanceImplementation()
    {
        var services = new DiServiceCollection();
        
        services.RegisterTransient<IGuidService, GuidService>(); // Register a transient service
        var container = services.GenerateContainer();
        
        var sut1 = container.GetService<IGuidService>();
        var sut2 = container.GetService<IGuidService>();
        
        Assert.NotEqual(sut1, sut2); // Our Service must be a different instance
    }
}