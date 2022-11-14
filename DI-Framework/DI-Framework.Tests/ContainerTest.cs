using DI_Framework.Exceptions;
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
        
        Assert.Throws<NotRegisteredException>(() => container.GetService<GuidService>()); // Must throw an Exception
    }



    [Fact]
    public void TestImbricateService()
    {
        var services = new DiServiceCollection();
        services.RegisterSingleton<FirstClass>();
        services.RegisterSingleton<ISecondClass, SecondClass>();
        services.RegisterSingleton<ThirdClass>();


        var container = services.GenerateContainer();

        var instanceFirstClass = container.GetService<FirstClass>();
        Assert.NotNull(instanceFirstClass?.SecondClass?.ThirdClass);
    }
    
    [Fact]
    public void TestImbricateServiceSingleton()
    {
        var services = new DiServiceCollection();
        services.RegisterTransient<FirstClass>();
        services.RegisterSingleton<ISecondClass, SecondClass>();
        services.RegisterTransient<ThirdClass>();

        
        var container = services.GenerateContainer();

        var instance1 = container.GetService<FirstClass>(); // Get 2 different instance of FirstClass
        var instance2 = container.GetService<FirstClass>(); // Get 2 different instance of FirstClass
        
        Assert.NotEqual(instance1, instance2); // Transient so must be different instances
        Assert.Equal(instance1?.SecondClass, instance2?.SecondClass); // Singleton so must be the same instance
    }
}

public class FirstClass
{
    public Guid Guid => System.Guid.NewGuid();
    public ISecondClass SecondClass { get; }
    public FirstClass(ISecondClass secondClass)
    {
        SecondClass = secondClass;
    }
}

public class SecondClass : ISecondClass
{
    public Guid Guid => System.Guid.NewGuid();
    public ThirdClass ThirdClass { get; }
    public SecondClass(ThirdClass thridClass)
    {
        ThirdClass = thridClass;
    }
}

public interface ISecondClass
{
    Guid Guid { get; }
    ThirdClass ThirdClass { get; }
}

public class ThirdClass
{
    public Guid Guid = System.Guid.NewGuid();
    public ThirdClass()
    {
    }
}