## Installation 💿

Vous pouvez installer la dernière version du package nuget en executant la commande `Install-Package DI-Framework -version 1.0.0-CI-20221111-212919` dans la console de votre gestionnaire de packages.

## Getting started 🚀

Dans un premier temps vous devez instancier votre container IOC comme ceci :

```csharp
var services = new DiServiceCollection();
```

Vous pouvez ensuite enregistrez vos différentes instances dans votre container. Il existe 3 types d'instances différentes.
 - Les Singletons
 - Les Transients
 - Les scopes

 À la fin de l'enregistrement de toutes vos instances, il faut générer votre container : 
 ```csharp
 var container = services.GenerateContainer();
 ```

 ### Singleton

 Les Singletons enregistreront qu'une seule instance tout au long de votre application. On peut les enregistrer de deux manières différentes (Avec ou sans interface) :
 ```csharp
 var services = new DiServiceCollection();
 services.RegisterSingleton<IGuidService, GuidService>(); // Register a singleton with an interface
 // ===== OR ====
 services.RegisterSingleton<GuidService>(); // Register a singleton without interface
var container = services.GenerateContainer();
 ```

 ### Transient
 Les transient vous fourniront une instance à chaque nouvelle demande ils s'enregistrent de cette façon :
  ```csharp
 var services = new DiServiceCollection();
 services.RegisterTransient<IGuidService, GuidService>(); // Register a transient with an interface
 // ===== OR ====
 services.RegisterTransient<GuidService>(); // Register a transient without interface
var container = services.GenerateContainer();
 ```

 ### Scope 
 Les scope fournisse une seule et même instance par scope. Il faut donc créer un scope pour pouvoir récupérer nos instances :
 ```csharp
var services = new DiServiceCollection();
services.RegisterScope<IGuidService, GuidService>();

var firstScope = services.CreateScope();  
var firstService = firstScope.Container.GetService<IGuidService>();
```
