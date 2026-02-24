using OpenQA.Selenium;
using PetClinic.UiTests.Pages;
using Reqnroll;

namespace PetClinic.UiTests.Steps
{
    [Binding]
    public class HomePageSteps
    {
        private readonly HomePage _homePage;

        public HomePageSteps( HomePage homePage)
        {
            this._homePage = homePage;
        }

        [Given("I am on the Home Page")]
        public void GivenIAmOnTheHomePage()
        {
            _homePage.VerifyIsAtHomePage();
        }
    }
}
