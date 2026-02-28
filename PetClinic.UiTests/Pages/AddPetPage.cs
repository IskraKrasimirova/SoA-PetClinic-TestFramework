using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PetClinic.UiTests.Models;
using PetClinic.UiTests.Utilities;
using PetClinic.UiTests.Utilities.Extensions;

namespace PetClinic.UiTests.Pages
{
    public class AddPetPage : BasePage
    {
        private IWebElement NewPetHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]//h2"));
        private IWebElement OwnerNameLabel => _driver.FindElement(By.XPath("//label[text()='Owner']/following-sibling::div"));
        private IWebElement NameInput => _driver.FindElement(By.XPath("//input[@id='name']"));
        private IWebElement BirthDateInput => _driver.FindElement(By.XPath("//input[@id='birthDate']"));
        private IWebElement TypeSelect => _driver.FindElement(By.XPath("//select[@id='type']"));
        private IWebElement AddPetButton => _driver.FindElement(By.XPath("//button[text()='Add Pet']"));
        private IWebElement LogoImage => _driver.FindElement(By.XPath("//img[contains(@src,'spring')]"));

        private ICollection<IWebElement> FieldValidationMessages(string fieldId) 
            => _driver.FindElements(By.XPath($"//*[@id='{fieldId}']/following::span[@class='help-inline']"));

        public AddPetPage(IWebDriver driver) : base(driver)
        {
        }

        public void AddNewPet(PetModel model)
        {
            NameInput.EnterText(model.Name);
            _driver.EnterDate(BirthDateInput, model.BirthDate);
            var select = new SelectElement(TypeSelect);

            if (!string.IsNullOrEmpty(model.Type))
            {
                select.SelectByText(model.Type);
            }

            AddPetButton.Click();
        }

        public string? GetFieldValidationMessage(string field)
        {
            var fieldId = GetFieldId(field);

            string? messageText = null;

            Retry.Until(() =>
            {
                var messages = FieldValidationMessages(fieldId);

                if (messages.Count == 0)
                    throw new RetryException("Validation message not yet present.");

                var actualMessage = messages.First();

                if (!actualMessage.Displayed)
                    throw new RetryException("Validation message is not visible yet.");

                messageText = actualMessage.Text.Trim();
            });

            return messageText;
        }

        public void VerifyOwnerName(string expectedFullName)
        {
            Assert.That(OwnerNameLabel.Text.Trim(), Is.EqualTo(expectedFullName));
        }

        public void VerifyIsAtAddPetPage()
        {
            _driver.WaitUntilUrlContains("pets/new");

            Assert.Multiple(() =>
            {
                Assert.That(NewPetHeader.Displayed, "NewPet header is not visible.");
                Assert.That(NewPetHeader.Text.Trim(), Is.EqualTo("New Pet"));
                Assert.That(OwnerNameLabel.Displayed, "Owner name label is not visible.");
                Assert.That(NameInput.Displayed, "Name field is not visible.");
                Assert.That(BirthDateInput.Displayed, "BirthDate field is not visible.");
                Assert.That(TypeSelect.Displayed, "Type dropdown is not visible.");
                Assert.That(AddPetButton.Displayed, "AddPet button is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
            });
        }

        private static string GetFieldId(string field)
        {
            string fieldId;

            switch (field)
            {
                case "Name":
                    fieldId = "name";
                    break;
                case "BirthDate":
                    fieldId = "birthDate";
                    break;
                case "Type":
                    fieldId = "type";
                    break;
                default:
                    throw new ArgumentException($"Unknown field: {field}");
            }

            return fieldId;
        }
    }
}
