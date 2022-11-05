using DI_Framework.Tests.Sut.Interfaces;
using DI_Framework.Tests.Sut.Services;
using Xunit;

namespace DI_Framework.Tests;

public class ScopeTest
{

    [Fact]
    public void TestScopeInstanceInterfaceAndImplementation()
    {
        var services = new DiServiceCollection();

        services.RegisterScope<IGuidService, GuidService>();

        var firstScope = services.CreateScope();
        var secondScope = services.CreateScope();   

        var firstService = firstScope.Container.GetService<IGuidService>();
        var sameFirstService = firstScope.Container.GetService<IGuidService>();

        var secondService = secondScope.Container.GetService<IGuidService>();

        Assert.NotEqual(firstService, secondService); // Deux scopes retournent une instance diff�rente pour une demande du m�me service
        Assert.Equal(firstService, sameFirstService); // L'instance est la m�me sur toute la port�e du scope
    }
    
    [Fact]
    public void TestScopeInstanceInterfaceAndImplementationWithSingleton()
    {
        var services = new DiServiceCollection();

        services.RegisterSingleton<IRandomGuidProvider, RandomGuidProvider>();
        services.RegisterScope<ClassWithoutInterface>();

        var firstScope = services.CreateScope();
        var secondScope = services.CreateScope();

        var firstService = firstScope.Container.GetService<ClassWithoutInterface>();
        var secondService = secondScope.Container.GetService<ClassWithoutInterface>();       

        Assert.NotEqual(firstService, secondService); // Deux scopes retournent une instance diff�rente pour une demande du m�me service
        Assert.Equal(firstService.GetGuid(), secondService.GetGuid()); // Deux scopes diff�rents r�cup�rent bien la m�me instance du singleton
    }
}