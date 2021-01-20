using System;
using ByteDev.Ioc.MsExtDi.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ByteDev.Ioc.MsExtDi.UnitTests
{
    public abstract class InstallerTestBase<TInstaller> where TInstaller : IServiceInstaller
    {
        protected readonly ServiceCollection ServiceCollection;
        protected readonly AppConfigurationBuilder AppConfigurationBuilder;

        protected InstallerTestBase()
        {
            ServiceCollection = new ServiceCollection();
            AppConfigurationBuilder = new AppConfigurationBuilder();
        }

        protected IServiceProvider CreateSut()
        {
            var configuration = AppConfigurationBuilder.Build();

            ServiceCollection.InstallFromAssemblyContaining<TInstaller>(configuration);

            return ServiceCollection.BuildServiceProvider();
        }
    }
}