using OpenQA.Selenium;
using PetClinic.UiTests.Utilities;
using PetClinic.UiTests.Utilities.Extensions;

namespace PetClinic.UiTests.Pages
{
    public class AddVisitPage : BasePage
    {
        private IWebElement NewVisitHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]//h2"));
        private IWebElement PetInfoTable => _driver.FindElement(By.XPath("//table[.//th[text()='Name']]"));
        private IWebElement DateInput => _driver.FindElement(By.XPath("//input[@id='date']"));
        private IWebElement DescriptionInput => _driver.FindElement(By.XPath("//input[@id='description']"));
        private IWebElement AddVisitButton => _driver.FindElement(By.XPath("//button[text()='Add Visit']"));
        private IWebElement LogoImage => _driver.FindElement(By.XPath("//img[contains(@src,'spring')]"));
        public AddVisitPage(IWebDriver driver) : base(driver)
        {

        }

        public void AddVisit(string date, string description)
        {
            _driver.EnterDate(DateInput, date);
            DescriptionInput.EnterText(description);

            AddVisitButton.Click();
        }

        public string GetFieldValidationMessage(string field)
        {

            IWebElement element;

            switch (field)
            {
                case "Date":
                    element = DateInput;
                    break;
                case "Description":
                    element = DescriptionInput;
                    break;
                default:
                    throw new ArgumentException($"Unknown field: {field}");
            }

            string? messageText = null;

            Retry.Until(() =>
            {
                var messages = element.FindElements(By.XPath("./parent::div/span[@class='help-inline']"));

                if (messages.Count == 0)
                    throw new RetryException("Message not loaded yet.");

                var actualMessage = messages.Last();

                if (!actualMessage.Displayed)
                    throw new RetryException("Message not visible yet.");

                messageText = actualMessage.Text.Trim();
            });

            return messageText;
        }

        public void VerifyIsAtAddVisitPage()
        {
            _driver.WaitUntilUrlContains("visits/new");

            Assert.Multiple(() =>
            {
                Assert.That(NewVisitHeader.Displayed, "NewVisit header is not visible.");
                Assert.That(NewVisitHeader.Text.Trim(), Is.EqualTo("New Visit"));
                Assert.That(PetInfoTable.Displayed, "PetInfo table is not visible.");
                Assert.That(DateInput.Displayed, "Date field is not visible.");
                Assert.That(DescriptionInput.Displayed, "Description field is not visible.");
                Assert.That(AddVisitButton.Displayed, "AddVisit button is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
            });
        }
    }
}
