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

        Assert.NotEqual(firstService, secondService); // Deux scopes retournent une instance différente pour une demande du même service
        Assert.Equal(firstService, sameFirstService); // L'instance est la même sur toute la portée du scope
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

        Assert.NotEqual(firstService, secondService); // Deux scopes retournent une instance différente pour une demande du même service
        Assert.Equal(firstService.GetGuid(), secondService.GetGuid()); // Deux scopes différents récupèrent bien la même instance du singleton
    }
}