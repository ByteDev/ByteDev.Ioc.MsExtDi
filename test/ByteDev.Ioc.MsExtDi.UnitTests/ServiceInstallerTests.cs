using ByteDev.Ioc.MsExtDi.Testing.Example;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace ByteDev.Ioc.MsExtDi.UnitTests
{
    [TestFixture]
    public class ServiceInstallerTests : InstallerTestBase<FoobarInstaller>
    {
        private const string FoobarUrl = "http://foobar.com/";

        [Test]
        public void WhenInstalled_ThenCanResolveFoobar()
        {
            var result = CreateSut().GetService<IFoobar>();

            Assert.That(result, Is.TypeOf<Foobar>());
        }


        [Test]
        public void WhenInstalled_ThenCanResolveFoobarSettings()
        {
            AppConfigurationBuilder.WithApplicationSetting("FoobarSettings:FoobarUrl", FoobarUrl);

            var result = CreateSut().GetService<FoobarSettings>();

            Assert.That(result.FoobarUrl, Is.EqualTo(FoobarUrl));
        }

        [Test]
        public void WhenInstalled_ThenCanResolveBar()
        {
            AppConfigurationBuilder.WithApplicationSetting("FoobarSettings:Bar:Name", "SomeBarName");

            var result = CreateSut().GetService<FoobarSettings>();

            Assert.That(result.Bar.Name, Is.EqualTo("SomeBarName"));
        }
        
        [Test]
        public void WhenInstalled_ThenCanResolveBars()
        {
            AppConfigurationBuilder.WithApplicationSetting("FoobarSettings:Bars:0:Name", "Bar1");
            AppConfigurationBuilder.WithApplicationSetting("FoobarSettings:Bars:1:Name", "Bar2");

            var result = CreateSut().GetService<FoobarSettings>();

            Assert.That(result.Bars[0].Name, Is.EqualTo("Bar1"));
            Assert.That(result.Bars[1].Name, Is.EqualTo("Bar2"));
        }
    }
}