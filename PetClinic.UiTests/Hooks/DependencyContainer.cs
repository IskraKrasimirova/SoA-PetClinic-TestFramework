using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PetClinic.UiTests.Models.Factory;
using PetClinic.UiTests.Pages;
using PetClinic.UiTests.Utilities;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

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
                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

                var options = new ChromeOptions();

                bool isCi = Environment.GetEnvironmentVariable("CI") == "true";

                if (isCi)
                {
                    options.AddArgument("--headless=new");
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--disable-dev-shm-usage");
                    options.AddArgument("--disable-gpu");
                    options.AddArgument("--window-size=1920,1080");
                }

                var driver = new ChromeDriver(options);

                if (!isCi)
                {
                    driver.Manage().Window.Maximize();
                }

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

                return driver;
            });

            services.AddSingleton<IOwnerFactory, OwnerFactory>();
            services.AddSingleton<IPetFactory, PetFactory>();

            RegisterPages(services);

            return services;
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
    }
}
