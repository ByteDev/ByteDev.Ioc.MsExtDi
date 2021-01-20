[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Ioc.MsExtDi?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Ioc-MsExtDi/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Ioc.MsExtDi.svg)](https://www.nuget.org/packages/ByteDev.Ioc.MsExtDi)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Ioc.MsExtDi/blob/master/LICENSE)

# ByteDev.Ioc.MsExtDi

Collection of extensions for Microsoft Extensions DI to help register dependencies and configure application settings.

## Installation

ByteDev.Ioc.MsExtDi has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Ioc.MsExtDi is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Ioc.MsExtDi`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Ioc.MsExtDi/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Ioc.MsExtDi/blob/master/docs/RELEASE-NOTES.md).

## Usage

**Add application settings**

For the purposes of configuration settings the library makes the assumption that all your application settings will be in your `appsettings.json` file within a section called `ApplicationSettings`.

From within the `ApplicationSettings` section you can then define your own sub sections:

```json
{
  "ApplicationSettings": {
    "FoobarSettings": {
      "FoobarUrl": "http://www.foorbarsomewhere.com/",
      "Bar": {
        "Name": "SomeName"
      }
    }
  }
}
```

These sub sections (e.g `FoobarSettings`) would then correspond to your own settings class:

```csharp
public class FoobarSettings
{
    public string FoobarUrl { get; set; }

    public Bar Bar { get; set; }
}

public class Bar
{
    public string Name { get; set; }
}
```

**Create installer classes**

Use installer classes to register your classes within the IoC container.  Installer classes help you separate your concerns of registering classes in the IoC container and the class implementation itself.

All installer classes must implement the `IServiceInstaller` interface.

You can also register settings classes using the `ConfigureSettings` extension method.

Example:

```csharp
public class FoobarInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IFoobar, Foobar>();

        services.ConfigureSettings<FoobarSettings>(configuration);
    }
}
```

**Install the installers**

Onnce you have your installer classes you can register them in the `ServiceCollection` using one of the extension methods:

- InstallFromAssembly
- InstallFromAssemblies
- InstallFromAssemblyContaining<T>

```csharp
// Use ConfigurationBuilder or AppConfigurationBuilder to create a IConfiguration

var serviceCollection = new ServiceCollection();

serviceCollection.InstallFromAssemblyContaining<FoobarInstaller>(configuration);
```

**Inject/resolve class dependencies**

Your registered class dependencies can now be injected, for example via constructor injection.

To resolve a registered dependency using service locator (anti)pattern:

```csharp
IServiceProvider provider = serviceCollection.BuildServiceProvider();

var foobar = provider.GetService<IFoobar>();

var foobarSettings = provider.GetService<FoobarSettings>()

var url = foobarSettings.FoobarUrl;
var name = foobarSettings.Bar.Name;
```

