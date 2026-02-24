using Microsoft.Extensions.Configuration;
using PetClinic.UiTests.Models;

namespace PetClinic.UiTests.Utilities
{
    public class ConfigurationManager
    {
        private static readonly Lazy<ConfigurationManager> lazy =
            new(() => new ConfigurationManager());

        public static ConfigurationManager Instance { get; } = lazy.Value;

        public SettingsModel SettingsModel { get; private set; }

        private ConfigurationManager()
        {
            var environment = Environment.GetEnvironmentVariable("environment", EnvironmentVariableTarget.User);
            var configurationFileName = string.IsNullOrEmpty(environment)
                ? "appsettings.json"
                : $"appsettings.{environment}.json";

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(configurationFileName, optional: true)
                .Build();

            SettingsModel = config.GetSection("Settings").Get<SettingsModel>()!;
        }
    }
}
