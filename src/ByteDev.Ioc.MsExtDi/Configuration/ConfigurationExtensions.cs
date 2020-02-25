using Microsoft.Extensions.Configuration;

namespace ByteDev.Ioc.MsExtDi.Configuration
{
    /// <summary>
    /// Extension methods for <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Retrieves the ApplicationSettings section.
        /// </summary>
        /// <param name="source">Configuration to get the AppSettings from.</param>
        /// <returns>AppSettings configuration.</returns>
        public static IConfiguration AppSettings(this IConfiguration source)
        {
            return source.GetSection("ApplicationSettings");
        }

        /// <summary>
        /// Retrieves the section from within ApplicationSettings.
        /// </summary>
        /// <typeparam name="TSettings">Settings type.</typeparam>
        /// <param name="source">Configuration to get the AppSettings section from.</param>
        /// <returns>AppSettings configuration section.</returns>
        public static IConfiguration AppSettingsSection<TSettings>(this IConfiguration source)
        {
            return source.AppSettings().GetSection(typeof(TSettings).Name);
        }
    }
}