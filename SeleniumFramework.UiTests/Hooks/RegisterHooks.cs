using OpenQA.Selenium;
using Reqnroll;

namespace SeleniumFramework.Hooks
{
    [Binding]
    public class RegisterHooks
    {
        private readonly IWebDriver _driver;

        public RegisterHooks(IWebDriver webDriver)
        {
            this._driver = webDriver;
        }

        [AfterScenario]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
