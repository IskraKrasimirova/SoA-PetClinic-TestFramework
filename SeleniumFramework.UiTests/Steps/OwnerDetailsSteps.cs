using Reqnroll;
using SeleniumFramework.Models;
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
    public class OwnerDetailsSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly OwnerDetailsPage _ownerDetailsPage;

        public OwnerDetailsSteps(ScenarioContext scenarioContext, OwnerDetailsPage ownerDetailsPage)
        {
            this._scenarioContext = scenarioContext;
            this._ownerDetailsPage = ownerDetailsPage;
        }

        [Then("the Owner Details page is displayed for the created owner")]
        public void ThenTheOwnerDetailsPageIsDisplayedForTheCreatedOwner()
        {
            _ownerDetailsPage.VerifyIsAtOwnerDetailsPage();

            var registeredOwner = _scenarioContext.Get<OwnerModel>(ContextConstants.RegisteredOwner);
            _ownerDetailsPage.VerifyOwnerDetails(registeredOwner);
        }

    }
}
