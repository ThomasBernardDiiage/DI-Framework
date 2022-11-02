using DI_Framework.Console;


var services = new DiServiceCollection();

//services.RegisterSingleton<RandomGuidGenerator>();
//services.RegisterTransient<ISomeService, SomeService>();
services.RegisterSingleton<IRandomGuidProvider, RandomGuidProvider>();

services.RegisterTransient<ISomeService, SomeService>();

var container = services.GenerateContainer();

//var serviceFirst = container.GetService<RandomGuidGenerator>();
//var serviceSecond = container.GetService<RandomGuidGenerator>();

var t1 = container.GetService<ISomeService>();
var t2 = container.GetService<ISomeService>();

t1.PrintSomething();
t2.PrintSomething();

//Console.WriteLine(serviceFirst.RandomGuid);
//Console.WriteLine(serviceSecond.RandomGuid);

Console.WriteLine("Hello, World!");
