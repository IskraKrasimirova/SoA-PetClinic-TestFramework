using OpenQA.Selenium;
using Reqnroll;
using SeleniumFramework.Pages;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class HomePageSteps
    {
        private readonly IWebDriver _driver;
        private readonly HomePage _homePage;

        public HomePageSteps(IWebDriver driver, HomePage homePage)
        {
            this._driver = driver;
            this._homePage = homePage;
        }

        [Given("I am on the Home Page")]
        public void GivenIAmOnTheHomePage()
        {
            _homePage.VerifyIsAtHomePage();
        }
    }
}
