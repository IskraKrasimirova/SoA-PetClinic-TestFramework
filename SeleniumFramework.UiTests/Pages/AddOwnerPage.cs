using OpenQA.Selenium;
using SeleniumFramework.Models;
using SeleniumFramework.Utilities;
using SeleniumFramework.Utilities.Extensions;

namespace SeleniumFramework.Pages
{
    public class AddOwnerPage : BasePage
    {
        private IWebElement NewOwnerHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]//h2"));
        private IWebElement FirstNameInput => _driver.FindElement(By.XPath("//input[@id='firstName']"));
        private IWebElement LastNameInput => _driver.FindElement(By.XPath("//input[@id='lastName']"));
        private IWebElement AddressInput => _driver.FindElement(By.XPath("//input[@id='address']"));
        private IWebElement CityInput => _driver.FindElement(By.XPath("//input[@id='city']"));
        private IWebElement TelerhoneInput => _driver.FindElement(By.XPath("//input[@id='telephone']"));
        private IWebElement AddOwnerButton => _driver.FindElement(By.XPath("//button[text()='Add Owner']"));
        private IWebElement LogoImage => _driver.FindElement(By.XPath("//img[contains(@src,'spring')]"));

        public AddOwnerPage(IWebDriver driver) : base(driver)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("document.querySelectorAll('input').forEach(i => i.setAttribute('autocomplete','off'));");
        }

        public void AddNewOwner(OwnerModel model)
        {
            FirstNameInput.EnterText(model.FirstName);
            LastNameInput.EnterText(model.LastName);
            AddressInput.EnterText(model.Address);
            CityInput.EnterText(model.City);
            TelerhoneInput.EnterText(model.Telephone);

            AddOwnerButton.Click();
        }

        public string? GetFieldValidationMessage(string field)
        {
            IWebElement element;

            switch (field)
            {
                case "FirstName":
                    element = FirstNameInput;
                    break;
                case "LastName":
                    element = LastNameInput;
                    break;
                case "Address":
                    element = AddressInput;
                    break;
                case "City":
                    element = CityInput;
                    break;
                case "Telephone":
                    element = TelerhoneInput;
                    break;
                default:
                    throw new ArgumentException($"Unknown field: {field}");
            }

            //Telephone mandatory field validation message is inconsistent
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

        public void VerifyIsAtAddOwnerPage()
        {
            _driver.WaitUntilUrlContains("owners/new");

            Assert.Multiple(() =>
            {
                Assert.That(NewOwnerHeader.Displayed, "NewOwner header is not visible.");
                Assert.That(NewOwnerHeader.Text.Trim(), Is.EqualTo("New Owner"));
                Assert.That(FirstNameInput.Displayed, "First Name field is not visible.");
                Assert.That(LastNameInput.Displayed, "Last Name field is not visible.");
                Assert.That(AddressInput.Displayed, "Address field is not visible.");
                Assert.That(CityInput.Displayed, "City field is not visible.");
                Assert.That(TelerhoneInput.Displayed, "Telerhone field is not visible.");
                Assert.That(AddOwnerButton.Displayed, "AddOwner button is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
            });
        }
    }
}
