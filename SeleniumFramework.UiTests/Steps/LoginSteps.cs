using OpenQA.Selenium;
using Reqnroll;
using SeleniumFramework.Models;
using SeleniumFramework.Pages;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;

        private readonly ScenarioContext _context;
        private readonly SettingsModel _settingsModel;

        public LoginSteps(ScenarioContext context, IWebDriver driver, LoginPage loginPage, SettingsModel model)
        {
            this._context = context;
            this._driver = driver;
            this._loginPage = loginPage;
            this._settingsModel = model;
        }

        [Given("I navigate to the main page")]
        public void GivenINavigateToTheMainPage()
        {
            _driver.Navigate().GoToUrl(_settingsModel.BaseUrl);
        }
    }
}
