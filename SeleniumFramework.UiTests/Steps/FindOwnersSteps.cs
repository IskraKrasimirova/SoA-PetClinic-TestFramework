using OpenQA.Selenium;
using Reqnroll;
using SeleniumFramework.Models;
using SeleniumFramework.Pages;
using SeleniumFramework.Utilities.Constants;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class FindOwnersSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FindOwnersPage _findOwnersPage;
        private readonly NavigationBar _navigationBar;

        public FindOwnersSteps(ScenarioContext scenarioContext, FindOwnersPage findOwnersPage, NavigationBar navigationBar)
        {
            this._scenarioContext = scenarioContext;
            this._findOwnersPage = findOwnersPage;
            this._navigationBar = navigationBar;
        }

        [Given("I navigate to Add Owner page")]
        public void GivenINavigateToAddOwnerPage()
        {
            _findOwnersPage.VerifyIsAtFindOwnersPage();
            _findOwnersPage.NavigateToAddOwnerPage();
        }

        [Given("I search for an owner with empty criteria")]
        public void GivenISearchForAnOwnerWithEmptyCriteria()
        {
            _findOwnersPage.VerifyIsAtFindOwnersPage();
            _findOwnersPage.NavigateToOwnersResultsPage();
        }

        [Then("I search for the created owner by Last Name")]
        public void ThenISearchForTheCreatedOwnerByLastName()
        {
            _navigationBar.GoToFindOwnersPage();
            _findOwnersPage.VerifyIsAtFindOwnersPage();

            var registeredOwner = _scenarioContext.Get<OwnerModel>(ContextConstants.RegisteredOwner);
            _findOwnersPage.SearchByLastName(registeredOwner.LastName);
        }
    }
}
