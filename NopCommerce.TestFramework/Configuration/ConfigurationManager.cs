using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NopCommerce.TestFramework.Configuration
{
    public static class ConfigurationManager
    {
        private static readonly Lazy<IConfigurationRoot> _configuration =
            new(BuildConfiguration);

        public static IConfigurationRoot Configuration => _configuration.Value;

        public static TestSettings Settings =>
            Configuration.Get<TestSettings>()
            ?? throw new InvalidOperationException(
                "Test configuration could not be loaded.");

        private static IConfigurationRoot BuildConfiguration()
        {
            var environment =
                Environment.GetEnvironmentVariable("TEST_ENVIRONMENT")
                ?? "Local";

            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    "appsettings.json",
                    optional: false,
                    reloadOnChange: false)
                .AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: true,
                    reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
