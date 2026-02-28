using PetClinic.UiTests.Models;
using PetClinic.UiTests.Models.Factory;
using PetClinic.UiTests.Pages;
using PetClinic.UiTests.Utilities.Constants;
using Reqnroll;

namespace PetClinic.UiTests.Steps
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

        [When("I try to create a new owner with invalid details for {string} with {string}")]
        public void WhenITryToCreateANewOwnerWithInvalidDetailsForWith(string field, string value)
        {
            _addOwnerPage.VerifyIsAtAddOwnerPage();
            
            var newOwner = _ownerFactory.CreateWith(field, value);
            _addOwnerPage.AddNewOwner(newOwner);
        }

        [When("I try to create a new owner without filling all mandatory fields")]
        public void WhenITryToCreateANewOwnerWithoutFillingAllMandatoryFields()
        {
            _addOwnerPage.VerifyIsAtAddOwnerPage();

            var newOwner = new OwnerModel()
            {
                FirstName = "",
                LastName = "",
                Address = "",
                City = "",
                Telephone = ""
            };

            _addOwnerPage.AddNewOwner(newOwner);
        }

        [Then("the owner appears in the search results with correct details")]
        public void ThenTheOwnerAppearsInTheSearchResultsWithCorrectDetails()
        {
            var registeredOwner = _scenarioContext.Get<OwnerModel>(ContextConstants.RegisteredOwner);

            if (_ownersResultsPage.IsAtOwnersResultsPage())
            {
                _ownersResultsPage.VerifyOwnerExists(registeredOwner);
            }
            else if (_ownerDetailsPage.IsAtOwnerDetailsPage())
            {
                _ownerDetailsPage.VerifyOwnerDetails(registeredOwner);
            }
            else
            {
                throw new Exception("Not on expected page after owner registration.");
            }
        }

        [Then("an appropriate error message {string} is displayed for field {string}")]
        public void ThenAnAppropriateErrorMessageIsDisplayedForField(string expectedMessage, string field)
        {
            var actualMessage = _addOwnerPage.GetFieldValidationMessage(field);

            if (field == "Telephone")
            {
                Assert.That(actualMessage, Is.EqualTo("numeric value out of bounds (<10 digits>.<0 digits> expected)").Or.EqualTo("must not be empty"), $"Validation message for field '{field}' is incorrect.");
            }
            else
            {
                Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Validation message for field '{field}' is incorrect.");
            }   
        }

        [Then("appropriate error messages are displayed for all mandatory fields")]
        public void ThenAppropriateErrorMessagesAreDisplayedForAllMandatoryFields()
        {
            Assert.Multiple(() => 
            { 
                Assert.That(_addOwnerPage.GetFieldValidationMessage("FirstName"), Is.EqualTo("must not be empty")); 
                Assert.That(_addOwnerPage.GetFieldValidationMessage("LastName"), Is.EqualTo("must not be empty")); 
                Assert.That(_addOwnerPage.GetFieldValidationMessage("Address"), Is.EqualTo("must not be empty")); 
                Assert.That(_addOwnerPage.GetFieldValidationMessage("City"), Is.EqualTo("must not be empty")); 
                Assert.That(_addOwnerPage.GetFieldValidationMessage("Telephone"), Is.EqualTo("numeric value out of bounds (<10 digits>.<0 digits> expected)").Or.EqualTo("must not be empty")); 
            });
        }
    }
}
