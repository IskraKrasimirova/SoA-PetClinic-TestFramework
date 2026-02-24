using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PetClinic.ApiTests.Apis;
using PetClinic.UiTests.DatabaseOperations.Operations;
using PetClinic.UiTests.Models;
using PetClinic.UiTests.Models.Factory;
using PetClinic.UiTests.Pages;
using PetClinic.UiTests.Utilities;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System.Data;

namespace PetClinic.UiTests.Hooks
{
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
            
            services.AddScoped<IWebDriver>(sp =>
            {
                //new DriverManager().SetUpDriver(new ChromeConfig());

                var driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

                return driver;
            });

            services.AddSingleton<IOwnerFactory, OwnerFactory>();
            services.AddSingleton<IPetFactory, PetFactory>();

            RegisterPages(services);
            //RegisterDatabaseOperations(services);
            RegisterApi(services);

            return services;
        }

        private static void RegisterDatabaseOperations(ServiceCollection services)
        {
            services.AddScoped<IDbConnection>(sp =>
            {
                var settings = sp.GetRequiredService<SettingsModel>();
                var connectionString = settings.ConnectionString;

                var dbConnection = new MySqlConnection(connectionString);
                return dbConnection;
            });

            services.AddScoped(sp =>
            {
                var dbConnection = sp.GetRequiredService<IDbConnection>();
                return new UserOperations(dbConnection);
            });
        }

        private static void RegisterPages(ServiceCollection services)
        {
            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new HomePage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new FindOwnersPage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new AddOwnerPage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new OwnerDetailsPage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new OwnersResultsPage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new AddPetPage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new AddVisitPage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new NavigationBar(driver);
            });
        }

        private static void RegisterApi(ServiceCollection services)
        {
            services.AddSingleton<RestClient>(sp =>
            {
                var settings = sp.GetRequiredService<SettingsModel>();
                var options = new RestClientOptions(settings.ApiBaseUrl);
                var client = new RestClient(options);
                client.AddDefaultHeader("Accept", "application/json");
                return client;
            });
        
            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<RestClient>();
                return new OwnersApi(client);
            });
        }
    }
}
