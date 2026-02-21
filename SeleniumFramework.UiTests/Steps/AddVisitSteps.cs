using Reqnroll;
using SeleniumFramework.Pages;
using SeleniumFramework.Utilities.Constants;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class AddVisitSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly AddVisitPage _addVisitPage;

        public AddVisitSteps(ScenarioContext scenarioContext, AddVisitPage addVisitPage)
        {
            this._scenarioContext = scenarioContext;
            this._addVisitPage = addVisitPage;
        }

        [When("I create a new visit with valid details")]
        public void WhenICreateANewVisitWithValidDetails()
        {
            _addVisitPage.VerifyIsAtAddVisitPage();

            var date = DateTime.Today.AddDays(5).ToString("yyyy-MM-dd");
            var description = $"Visit {Guid.NewGuid()}";

            _addVisitPage.AddVisit(date, description);

            _scenarioContext[ContextConstants.VisitDescription] = description;
        }
    }
}
