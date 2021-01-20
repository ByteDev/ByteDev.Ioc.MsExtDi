using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ByteDev.Ioc.MsExtDi.IntTests
{
    public abstract class InstallerTestBase<TInstaller> where TInstaller : IServiceInstaller
    {
        protected readonly ServiceCollection ServiceCollection;
        protected IConfigurationBuilder ConfigBuilder;

        protected InstallerTestBase()
        {
            ServiceCollection = new ServiceCollection();
            ConfigBuilder = new ConfigurationBuilder();
        }

        protected IServiceProvider CreateSut()
        {
            ConfigBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = ConfigBuilder.Build();

            ServiceCollection.InstallFromAssemblyContaining<TInstaller>(configuration);

            return ServiceCollection.BuildServiceProvider();
        }
    }
}