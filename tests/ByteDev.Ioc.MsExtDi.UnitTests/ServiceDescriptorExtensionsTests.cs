using NUnit.Framework;

namespace ByteDev.Ioc.MsExtDi.UnitTests
{
    [TestFixture]
    public class ServiceDescriptorExtensionsTests
    {
        [TestFixture]
        public class GetConfigurationInstance : ServiceDescriptorExtensionsTests
        {
            [Test]
            public void WhenSourceIsNull_ThenReturnNull()
            {
                var result = ServiceDescriptorExtensions.GetConfigurationInstance(null);

                Assert.That(result, Is.Null);
            }
        }
    }
}