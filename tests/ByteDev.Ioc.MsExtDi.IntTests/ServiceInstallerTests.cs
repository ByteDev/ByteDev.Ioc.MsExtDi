using ByteDev.Ioc.MsExtDi.Testing.Example;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ByteDev.Ioc.MsExtDi.IntTests
{
    [TestFixture]
    public class ServiceInstallerTests : InstallerTestBase<FoobarInstaller>
    {
        [TestAttribute]
        public void WhenInstalled_ThenCanResolveFoobarSettings()
        {
            var result = CreateSut().GetService<FoobarSettings>();

            Assert.That(result.FoobarUrl, Is.EqualTo("http://www.foorbarsomewhere.com/"));
        }

        [Test]
        public void WhenInstalled_ThenCanResolveBar()
        {
            var result = CreateSut().GetService<FoobarSettings>();

            Assert.That(result.Bar.Name, Is.EqualTo("SomeName"));
        }

        [Test]
        public void WhenInstalled_ThenCanResolveBars()
        {
            var result = CreateSut().GetService<FoobarSettings>();

            Assert.That(result.Bars[0].Name, Is.EqualTo("Bar1"));
            Assert.That(result.Bars[1].Name, Is.EqualTo("Bar2"));
        }
    }

    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration
}