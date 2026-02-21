using Reqnroll;
using SeleniumFramework.Models;
using SeleniumFramework.Models.Factory;
using SeleniumFramework.Pages;
using SeleniumFramework.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class PetRegistrationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPetFactory _petFactory;
        private readonly AddPetPage _addPetPage;

        public PetRegistrationSteps(ScenarioContext scenarioContext, IPetFactory petFactory, AddPetPage addPetPage)
        {
            this._scenarioContext = scenarioContext;
            this._petFactory = petFactory;
            this._addPetPage = addPetPage;
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
    }
}
