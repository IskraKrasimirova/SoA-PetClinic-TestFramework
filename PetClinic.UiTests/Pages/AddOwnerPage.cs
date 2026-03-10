using OpenQA.Selenium;
using PetClinic.UiTests.Models;
using PetClinic.UiTests.Utilities;
using PetClinic.UiTests.Utilities.Extensions;

namespace PetClinic.UiTests.Pages
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

        private ICollection<IWebElement> FieldValidationMessages(string fieldId)
            => _driver.FindElements(By.XPath($"//*[@id='{fieldId}']/parent::div/span[@class='help-inline']"));

        public AddOwnerPage(IWebDriver driver) : base(driver)
        {
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
            string? messageText = null;

            Retry.Until(() =>
            {
                var fieldId = GetFieldId(field);
                var messages = FieldValidationMessages(fieldId);

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

        private static string GetFieldId(string field)
        {
            string fieldId;

            switch (field)
            {
                case "FirstName":
                    fieldId = "firstName";
                    break;
                case "LastName":
                    fieldId = "lastName";
                    break;
                case "Address":
                    fieldId = "address";
                    break;
                case "City":
                    fieldId = "city";
                    break;
                case "Telephone":
                    fieldId = "telephone";
                    break;
                default:
                    throw new ArgumentException($"Unknown field: {field}");
            }

            return fieldId;
        }
    }
}
