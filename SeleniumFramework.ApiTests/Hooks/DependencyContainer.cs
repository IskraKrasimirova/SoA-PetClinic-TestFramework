using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using RestSharp;
using SeleniumFramework.ApiTests.Apis;
using SeleniumFramework.ApiTests.Models;
using SeleniumFramework.ApiTests.Utils;
using SeleniumTestFramework.UiTests.Models.Builders;

namespace SeleniumFramework.ApiTests.Hooks;

public class DependencyContainer
{
    [ScenarioDependencies]
    public static IServiceCollection RegisterDependencies()
    {
        var services = new ServiceCollection();
        services.AddSingleton(sp =>
        {
            return ConfigurationManager.Instance.SettingsModel;
        });

        services.AddSingleton<RestClient>(sp =>
        {
            var settings = sp.GetRequiredService<SettingsModel>();
            var options = new RestClientOptions(settings.BaseUrl);
            var client = new RestClient(options);
            client.AddDefaultHeader("Accept", "application/json");
            return client;
        });
        
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<RestClient>();
            return new OwnersApi(client);
        });

        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<RestClient>();
            return new PetTypesApi(client);
        });

        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<RestClient>();
            return new PetsApi(client);
        });

        services.AddScoped<OwnerBuilder>();

        return services;
    }
}