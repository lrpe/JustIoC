# JustIoC

Just a simple IoC container, that I am implementing as a hobby project.

Current features:
- Register services as **concrete types** or using an **interface**.
- **Basic** support for **constructor injection**.
- **Only singletons** are supported for now.

## Usage

Instantiate a new container:

```csharp
var just = new JustContainer();
```

Register services, with or without interfaces:

```csharp
just.Add<IMyService, MyService>();
```
```csharp
just.Add<MyConcreteService>();
```

Resolve dependencies:

```csharp
var myService = just.Get<IMyService>();
```

Inject services using constructor parameters:

```csharp
public class DependentClass
{
  private readonly IMyService;

  DependentClass(IMyService myService)
  {
    _myService = myService;
  }
}
```
