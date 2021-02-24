using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ByteDev.Ioc.MsExtDi
{
    /// <summary>
    /// Extension methods for <see cref="T:Microsoft.Extensions.DependencyInjection.ServiceDescriptor" />.
    /// </summary>
    public static class ServiceDescriptorExtensions
    {
        /// <summary>
        /// Get the implementation instance from the descriptor cast as IConfiguration.
        /// </summary>
        /// <param name="source">Service descriptor to use.</param>
        /// <returns>Instance of configuration or null if none can be found.</returns>
        public static IConfiguration GetConfigurationInstance(this ServiceDescriptor source)
        {
            if (source == null)
                return null;

            var config = (IConfiguration)source.ImplementationInstance;

            if (config != null)
                return config;
                    
            return (IConfiguration)source.ImplementationFactory(null);
        }
    }
}