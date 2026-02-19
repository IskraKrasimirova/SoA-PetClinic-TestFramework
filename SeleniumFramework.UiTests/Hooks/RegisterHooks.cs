using OpenQA.Selenium;
using Reqnroll;
using SeleniumFramework.Models;

namespace SeleniumFramework.Hooks
{
    [Binding]
    public class RegisterHooks
    {
        private readonly IWebDriver _driver;
        private readonly SettingsModel _settingsModel;

        public RegisterHooks(IWebDriver webDriver, SettingsModel settingsModel)
        {
            this._driver = webDriver;
            this._settingsModel = settingsModel;
        }

        [BeforeScenario]
        public void NavigateToMainPage()
        {
            _driver.Navigate().GoToUrl(_settingsModel.BaseUrl);
        }

        [AfterScenario]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
