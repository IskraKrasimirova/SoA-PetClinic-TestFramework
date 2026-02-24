using PetClinic.UiTests.Models;
using PetClinic.UiTests.Pages;
using PetClinic.UiTests.Utilities.Constants;
using Reqnroll;

namespace PetClinic.UiTests.Steps
{
    [Binding]
    public class OwnerDetailsSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly OwnerDetailsPage _ownerDetailsPage;

        public OwnerDetailsSteps(ScenarioContext scenarioContext, OwnerDetailsPage ownerDetailsPage)
        {
            this._scenarioContext = scenarioContext;
            this._ownerDetailsPage = ownerDetailsPage;
        }

        [Given("the Owner Details page is displayed for the created owner")]
        [Then("the Owner Details page is displayed for the created owner")]
        public void ThenTheOwnerDetailsPageIsDisplayedForTheCreatedOwner()
        {
            _ownerDetailsPage.VerifyIsAtOwnerDetailsPage();

            var registeredOwner = _scenarioContext.Get<OwnerModel>(ContextConstants.RegisteredOwner);
            _ownerDetailsPage.VerifyOwnerDetails(registeredOwner);
        }

        [Given("the Owner Details page is displayed for the selected owner")]
        public void GivenTheOwnerDetailsPageIsDisplayedForTheSelectedOwner()
        {
            _ownerDetailsPage.VerifyIsAtOwnerDetailsPage();
        }

        [When("I navigate to Add Visit page for the first pet")]
        public void WhenINavigateToAddVisitPageForTheFirstPet()
        {
            _ownerDetailsPage.ClickAddVisitForTheFirstPet();
        }

        [When("I navigate to Add New Pet page")]
        public void WhenINavigateToAddNewPetPage()
        {
            _ownerDetailsPage.NavigateToAddNewPet();
        }

        [Then("the pet is displayed in the Owner Details page")]
        public void ThenThePetIsDisplayedInTheOwnerDetailsPage()
        {
            _ownerDetailsPage.VerifyIsAtOwnerDetailsPage();

            var registeredOwner = _scenarioContext.Get<OwnerModel>(ContextConstants.RegisteredOwner);
            _ownerDetailsPage.VerifyOwnerDetails(registeredOwner);

            var registeredPet = _scenarioContext.Get<PetModel>(ContextConstants.RegisteredPet);
            _ownerDetailsPage.VerifyPetDetails(registeredPet);
        }

        [Then("the visit is diplayed in the Owner Details page")]
        public void ThenTheVisitIsDiplayedInTheOwnerDetailsPage()
        {
            _ownerDetailsPage.VerifyIsAtOwnerDetailsPage();

            var visitDescription = _scenarioContext.Get<string>(ContextConstants.VisitDescription);

            _ownerDetailsPage.VerifyVisitExists(visitDescription);
        }
    }
}
