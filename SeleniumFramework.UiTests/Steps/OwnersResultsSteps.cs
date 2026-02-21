using Reqnroll;
using SeleniumFramework.Pages;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class OwnersResultsSteps
    {
        private readonly OwnersResultsPage _ownersResultsPage;

        public OwnersResultsSteps(OwnersResultsPage ownersResultsPage)
        {
            this._ownersResultsPage = ownersResultsPage;
        }

        [Given("I select the first owner from the results")]
        public void GivenISelectTheFirstOwnerFromTheResults()
        {
            _ownersResultsPage.VerifyIsAtOwnersResultsPage();
            _ownersResultsPage.SelectFirstOwner();
        }
    }
}
