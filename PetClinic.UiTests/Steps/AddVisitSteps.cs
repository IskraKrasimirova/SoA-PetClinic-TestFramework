using PetClinic.UiTests.Pages;
using PetClinic.UiTests.Utilities.Constants;
using Reqnroll;

namespace PetClinic.UiTests.Steps
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

        [When("I try to create a new visit with invalid details for {string} with {string}")]
        public void WhenITryToCreateANewVisitWithInvalidDetailsForWith(string field, string value)
        {
            _addVisitPage.VerifyIsAtAddVisitPage();

            string date;
            string description;

            switch (field)
            {
                case "Date":
                    date = value;
                    description = $"Visit {Guid.NewGuid()}";
                    break;
                case "Description":
                    date = DateTime.Today.AddDays(5).ToString("yyyy-MM-dd");
                    description = value;
                    break;
                default:
                    throw new ArgumentException($"Unknown field: {field}");
            }

            _addVisitPage.AddVisit(date, description);
        }

        [Then("a proper error message {string} is shown for field {string}")]
        public void ThenAProperErrorMessageIsShownForField(string expectedMessage, string field)
        {
            var actualMessage = _addVisitPage.GetFieldValidationMessage(field);

            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Validation message for field '{field}' is incorrect.");
        }
    }
}
