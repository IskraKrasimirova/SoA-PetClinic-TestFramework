using Reqnroll;
using SeleniumFramework.Pages;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class NavigationSteps
    {
        private readonly NavigationBar _navigationBar;

        public NavigationSteps(NavigationBar navigationBar)
        {
            this._navigationBar = navigationBar;
        }

        [Given("I navigate to Find Owners page")]
        [When("I navigate to Find Owners page")]
        public void WhenINavigateToFindOwnersPage()
        {
            _navigationBar.VerifyNavigationIsVisible();
            _navigationBar.GoToFindOwnersPage();
        }

    }
}
