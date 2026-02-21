using Reqnroll;
using SeleniumFramework.Models;
using SeleniumFramework.Models.Factory;
using SeleniumFramework.Pages;
using SeleniumFramework.Utilities.Constants;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class OwnerRegistrationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly AddOwnerPage _addOwnerPage;
        private readonly OwnerDetailsPage _ownerDetailsPage;
        private readonly OwnersResultsPage _ownersResultsPage;
        private readonly IOwnerFactory _ownerFactory;

        public OwnerRegistrationSteps(ScenarioContext scenarioContext, AddOwnerPage addOwnerPage,  IOwnerFactory ownerFactory, OwnerDetailsPage ownerDetailsPage, OwnersResultsPage ownersResultsPage)
        {
            this._scenarioContext = scenarioContext;
            this._addOwnerPage = addOwnerPage;
            this._ownerFactory = ownerFactory;
            this._ownerDetailsPage = ownerDetailsPage;
            this._ownersResultsPage = ownersResultsPage;
        }

        [Given("I create a new owner with valid details")]
        [When("I create a new owner with valid details")]
        public void WhenICreateANewOwnerWithValidDetails()
        {
            _addOwnerPage.VerifyIsAtAddOwnerPage();

            var newOwner = _ownerFactory.CreateDefault();
            _addOwnerPage.AddNewOwner(newOwner);

            _scenarioContext[ContextConstants.RegisteredOwner] = newOwner;
        }

        [Then("the owner appears in the search results with correct details")]
        public void ThenTheOwnerAppearsInTheSearchResultsWithCorrectDetails()
        {
            var registeredOwner = _scenarioContext.Get<OwnerModel>(ContextConstants.RegisteredOwner);
            var fullName = $"{registeredOwner.FirstName} {registeredOwner.LastName}";

            if (_ownersResultsPage.IsAtOwnersResultsPage())
            {
                _ownersResultsPage.VerifyOwnerExists(fullName);
            }
            else if(_ownerDetailsPage.IsAtOwnerDetailsPage())
            {
                _ownerDetailsPage.VerifyOwnerDetails(registeredOwner);
            }
        }
    }
}
