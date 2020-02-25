using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ByteDev.Ioc.MsExtDi
{
    /// <summary>
    /// Represents the interface for a service installer of types.
    /// </summary>
    public interface IServiceInstaller
    {
        /// <summary>
        /// Registers a set of types within the IoC container.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Configuration to apply.</param>
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}