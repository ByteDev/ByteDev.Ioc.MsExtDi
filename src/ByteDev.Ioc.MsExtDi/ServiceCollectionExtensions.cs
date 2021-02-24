using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ByteDev.Ioc.MsExtDi.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ByteDev.Ioc.MsExtDi
{
    /// <summary>
    /// Extension methods for <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Install all types that implement <see cref="T:ByteDev.Ioc.MsExtDi.IServiceInstaller" /> in <paramref name="assemblies" />.
        /// </summary>
        /// <param name="source">Service collection to install on.</param>
        /// <param name="configuration">Configuration to apply when performing the install.</param>
        /// <param name="assemblies">Assembly containing all types that implement <see cref="T:ByteDev.Ioc.MsExtDi.IServiceInstaller" />.</param>
        /// <returns>Reference to <paramref name="source" />.</returns>
        public static IServiceCollection InstallFromAssemblies(this IServiceCollection source, IConfiguration configuration, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                source.InstallFromAssembly(configuration, assembly);
            }

            return source;
        }

        /// <summary>
        /// Install all types that implement <see cref="T:ByteDev.Ioc.MsExtDi.IServiceInstaller" /> from assembly containing type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Containing type.</typeparam>
        /// <param name="source">Service collection to install on.</param>
        /// <param name="configuration">Configuration to apply when performing the install.</param>
        /// <returns>Reference to <paramref name="source" />.</returns>
        public static IServiceCollection InstallFromAssemblyContaining<T>(this IServiceCollection source, IConfiguration configuration)
        {
            return source.InstallFromAssembly(configuration, typeof(T).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Installs all types that implement <see cref="T:ByteDev.Ioc.MsExtDi.IServiceInstaller" /> in <paramref name="assembly" />.
        /// </summary>
        /// <param name="source">Service collection to install on.</param>
        /// <param name="configuration">Configuration to apply when performing the install.</param>
        /// <param name="assembly">Assembly containing all types that implement <see cref="T:ByteDev.Ioc.MsExtDi.IServiceInstaller" />.</param>
        /// <returns>Reference to <paramref name="source" />.</returns>
        public static IServiceCollection InstallFromAssembly(this IServiceCollection source, IConfiguration configuration, Assembly assembly)
        {
            var types = assembly.ExportedTypes.Where(t => t.IsInstaller()).ToArray();
            var installers = types.Select(Activator.CreateInstance).Cast<IServiceInstaller>();

            foreach (var installer in installers)
            {
                installer.Install(source, configuration);
            }

            return source;
        }

        /// <summary>
        /// Configure <paramref name="source" /> with the settings in the ApplicationSettings section. 
        /// </summary>
        /// <typeparam name="TSettings">Settings type.</typeparam>
        /// <param name="source">Service collection to config settings on.</param>
        /// <param name="configuration">Configuration to apply when performing the operation on.</param>
        /// <returns>Reference to <paramref name="source" />.</returns>
        public static IServiceCollection ConfigureSettings<TSettings>(this IServiceCollection source, IConfiguration configuration)
            where TSettings : class, new()
        {
            source.AddOptions();

            source.Configure<TSettings>(configuration.AppSettingsSection<TSettings>());

            source.AddSingleton(s => s.GetService<IOptions<TSettings>>().Value);

            return source;
        }

        /// <summary>
        /// Get configuration implementation. If no configuration can be found null will be returned.
        /// If more than one configuration is found then an exception is thrown.
        /// </summary>
        /// <param name="source">Service collection to retrieve configuration from.</param>
        /// <returns>ICollection implementation.</returns>
        /// <exception cref="T:System.InvalidOperationException">Service collection contains more than one IConfiguration.</exception>
        public static IConfiguration GetConfiguration(this IServiceCollection source)
        {
            if (source == null)
                return null;

            ServiceDescriptor descriptor;

            try
            {
                descriptor = source.SingleOrDefault(p => p.ServiceType == typeof(IConfiguration));
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Service collection contains more than one IConfiguration.", ex);
            }

            return descriptor.GetConfigurationInstance();
        }

        /// <summary>
        /// Get all IConfiguration service descriptors.
        /// </summary>
        /// <param name="source">Service collection to retrieve configuration descriptors from.</param>
        /// <returns>Collection of IConfiguration service descriptors.</returns>
        public static IEnumerable<ServiceDescriptor> GetConfigurationDescriptors(this IServiceCollection source)
        {
            if (source == null)
                return Enumerable.Empty<ServiceDescriptor>();

            return source.Where(p => p.ServiceType == typeof(IConfiguration));
        }
    }
}