using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace ByteDev.Ioc.MsExtDi.Configuration
{
    /// <summary>
    /// Wrapper class around the ConfigurationBuilder for the purposes
    /// of adding name value pairs to the ApplicationSettings section.
    /// </summary>
    public class AppConfigurationBuilder
    {
        private readonly IList<KeyValuePair<string, string>> _initialData = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Add ApplicationSettings key value pair setting.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <returns>Current instance of <see cref="T:ByteDev.Ioc.MsExtDi.Configuration.AppConfigurationBuilder" />.</returns>
        public AppConfigurationBuilder WithApplicationSetting(string key, string value)
        {
            WithSetting($"ApplicationSettings:{key}", value);
            return this;
        }

        /// <summary>
        /// Add key value pair setting.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <returns>Current instance of <see cref="T:ByteDev.Ioc.MsExtDi.Configuration.AppConfigurationBuilder" />.</returns>
        public AppConfigurationBuilder WithSetting(string key, string value)
        {
            _initialData.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        /// <summary>
        /// Builds a new instance of <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" />.
        /// </summary>
        /// <returns>An <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" /> with added keys and values.</returns>
        public IConfiguration Build()
        {
            return new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource
                {
                    InitialData = _initialData.ToArray()
                })
                .Build();
        }
    }
}