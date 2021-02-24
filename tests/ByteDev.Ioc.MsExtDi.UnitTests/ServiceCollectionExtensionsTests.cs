using System;
using System.Linq;
using ByteDev.Ioc.MsExtDi.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ByteDev.Ioc.MsExtDi.UnitTests
{
    [TestFixture]
    public class ServiceCollectionExtensionsTests
    {
        private ServiceCollection _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ServiceCollection();
        }

        public class TestSettings
        {
            public string Key1 { get; set; }
        }

        [TestFixture]
        public class GetConfiguration : ServiceCollectionExtensionsTests
        {
            [Test]
            public void WhenSourceIsNull_ThenReturnNull()
            {
                var result = ServiceCollectionExtensions.GetConfiguration(null);

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenConfigurationDoesNotExist_ThenReturnNull()
            {
                var result = _sut.GetConfiguration();

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenConfigurationExists_ThenReturnConfiguration()
            {
                IConfiguration config = new AppConfigurationBuilder()
                    .WithSetting("Key1", "Value1")
                    .Build();

                _sut.AddSingleton(config);

                var result = _sut.GetConfiguration();

                Assert.That(result.Get<TestSettings>().Key1, Is.EqualTo("Value1"));
            }

            [Test]
            public void WhenTwoConfigurationsExist_ThenThrowException()
            {
                IConfiguration config1 = new AppConfigurationBuilder().Build();
                IConfiguration config2 = new AppConfigurationBuilder().Build();

                _sut.AddSingleton(config1);
                _sut.AddSingleton(config2);

                var ex = Assert.Throws<InvalidOperationException>(() => _sut.GetConfiguration());
                Assert.That(ex.Message, Is.EqualTo("Service collection contains more than one IConfiguration."));
            }
        }

        [TestFixture]
        public class GetConfigurationDescriptors : ServiceCollectionExtensionsTests
        {
            [Test]
            public void WhenSourceIsNull_ThenReturnEmpty()
            {
                var result = ServiceCollectionExtensions.GetConfigurationDescriptors(null);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenContainsNoConfigurationDescriptors_ThenReturnEmpty()
            {
                var result = _sut.GetConfigurationDescriptors();

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenContainsConfigurationDescriptors_ThenReturnDescriptors()
            {
                IConfiguration config1 = new AppConfigurationBuilder().Build();
                IConfiguration config2 = new AppConfigurationBuilder().Build();

                _sut.AddSingleton(config1);
                _sut.AddSingleton(config2);

                var result = _sut.GetConfigurationDescriptors();

                Assert.That(result.Count(), Is.EqualTo(2));
            }
        }
    }
}