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
         
         
         private abstract class SutAbstractClass
         {
         }
         
         private interface SutInterface
         {
         }
         
         //[Fact]
         //public void TestAbstractOrInterface()
         //{
         //    var services = new DiServiceCollection();
        //
         //    Assert.Throws<AbstractOrInterfaceClassException>(() => services.RegisterSingleton<SutAbstractClass>()); // Must throw an Exception (we can't register abstract classes)
          //   Assert.Throws<AbstractOrInterfaceClassException>(() => services.RegisterSingleton<SutInterface>()); // Must throw exception if we use an Interface
             
          //   services.GenerateContainer(); // Generate container without register our service
         //}
    
    
}