using Reqnroll;
using SeleniumFramework.Models;
using SeleniumFramework.Models.Factory;
using SeleniumFramework.Pages;
using SeleniumFramework.Utilities.Constants;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class PetRegistrationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPetFactory _petFactory;
        private readonly AddPetPage _addPetPage;
        private readonly FindOwnersPage _findOwnersPage;
        private readonly OwnersResultsPage _ownersResultsPage;
        private readonly OwnerDetailsPage _ownerDetailsPage;

        public PetRegistrationSteps(ScenarioContext scenarioContext, IPetFactory petFactory, AddPetPage addPetPage, FindOwnersPage findOwnersPage, OwnersResultsPage ownersResultsPage, OwnerDetailsPage ownerDetailsPage)
        {
            this._scenarioContext = scenarioContext;
            this._petFactory = petFactory;
            this._addPetPage = addPetPage;
            this._findOwnersPage = findOwnersPage;
            this._ownersResultsPage = ownersResultsPage;
            this._ownerDetailsPage = ownerDetailsPage;
        }

        [Given("I am on the Add New Pet page for an existing owner")]
        public void GivenIAmOnTheAddNewPetPageForAnExistingOwner()
        {
            _findOwnersPage.VerifyIsAtFindOwnersPage();
            _findOwnersPage.NavigateToOwnersResultsPage();

            _ownersResultsPage.VerifyIsAtOwnersResultsPage();
            _ownersResultsPage.SelectFirstOwner();

            _ownerDetailsPage.VerifyIsAtOwnerDetailsPage();
            _ownerDetailsPage.NavigateToAddNewPet();

            _addPetPage.VerifyIsAtAddPetPage();
        }

        [Given("the owner already has a pet with given name")]
        public void GivenTheOwnerAlreadyHasAPetWithGivenName()
        {
            var newPet = _petFactory.CreateDefault();
            _addPetPage.AddNewPet(newPet);

            _scenarioContext[ContextConstants.RegisteredPet] = newPet;

            _ownerDetailsPage.VerifyIsAtOwnerDetailsPage();
            _ownerDetailsPage.NavigateToAddNewPet();

            _addPetPage.VerifyIsAtAddPetPage();
        }

        [When("I create a new pet with valid details")]
        public void WhenICreateANewPetWithValidDetails()
        {
            _addPetPage.VerifyIsAtAddPetPage();

            var registeredOwner = _scenarioContext.Get<OwnerModel>(ContextConstants.RegisteredOwner);
            var ownerFullName = $"{registeredOwner.FirstName} {registeredOwner.LastName}";
            _addPetPage.VerifyOwnerName(ownerFullName);

            var newPet = _petFactory.CreateDefault();
            _addPetPage.AddNewPet(newPet);

            _scenarioContext[ContextConstants.RegisteredPet] = newPet;
        }

        [When("I try to create a new pet with invalid details for {string} with {string}")]
        public void WhenITryToCreateANewPetWithInvalidDetailsForWith(string field, string value)
        {
            if (field == "Name" && !string.IsNullOrWhiteSpace(value))
            {
                value = $"{value}{Guid.NewGuid().ToString("N").Substring(0, 4)}";
            }

            var newPet = _petFactory.CreateWith(field, value);
            _addPetPage.AddNewPet(newPet);
        }

        [When("I try to add a pet without filling all mandatory fields")]
        public void WhenITryToAddAPetWithoutFillingAllMandatoryFields()
        {
            var newPet = new PetModel
            {
                Name = string.Empty,
                BirthDate = string.Empty,
                Type = string.Empty
            };

            _addPetPage.AddNewPet(newPet);
        }

        [When("I try to add a new pet with the same name")]
        public void WhenITryToAddANewPetWithTheSameName()
        {
            var existingPet = _scenarioContext.Get<PetModel>(ContextConstants.RegisteredPet);

            var newPet = _petFactory.CreateWith("Name", existingPet.Name);
            _addPetPage.AddNewPet(newPet);
        }

        [Then("an appropriate error message {string} is shown for field {string}")]
        public void ThenAnAppropriateErrorMessageIsShownForField(string expectedMessage, string field)
        {
            var actualMessage = _addPetPage.GetFieldValidationMessage(field);

            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Validation message for field '{field}' is incorrect.");
        }

        [Then("validation messages are displayed for all mandatory fields")]
        public void ThenValidationMessagesAreDisplayedForAllMandatoryFields()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_addPetPage.GetFieldValidationMessage("Name"), Is.EqualTo("is required"));
                Assert.That(_addPetPage.GetFieldValidationMessage("BirthDate"), Is.EqualTo("is required"));
                Assert.That(_addPetPage.GetFieldValidationMessage("Type"), Is.EqualTo("is required"));
            });
        }
    }
}
