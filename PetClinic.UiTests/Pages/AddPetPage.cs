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
            IWebElement element;

            switch (field)
            {
                case "Name":
                    element = NameInput;
                    break;
                case "BirthDate":
                    element = BirthDateInput;
                    break;
                case "Type":
                    element = TypeSelect;
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

                var actualMessage = messages.First();

                if (!actualMessage.Displayed)
                    throw new RetryException("Message not visible yet.");

                messageText = actualMessage.Text.Trim();
            },  waitInMilliseconds: 700);

            return messageText;
        }

        //public string? GetFieldValidationMessage(string field)
        //{
        //    string fieldId = field switch
        //    {
        //        "Name" => "name",
        //        "BirthDate" => "birthDate",
        //        "Type" => "type",
        //        _ => throw new ArgumentException($"Unknown field: {field}")
        //    };

        //    // Най-стабилният възможен локатор:
        //    // намира първия help-inline след елемента, без значение структурата на DOM
        //    string xpath = $"//*[@id='{fieldId}']/following::span[contains(@class,'help-inline')][1]";

        //    string? messageText = null;

        //    Retry.Until(() =>
        //    {
        //        var messages = _driver.FindElements(By.XPath(xpath));

        //        if (messages.Count == 0)
        //            throw new RetryException("Validation message not yet present.");

        //        var actualMessage = messages.First();

        //        if (!actualMessage.Displayed)
        //            throw new RetryException("Validation message is in DOM but not visible yet.");

        //        messageText = actualMessage.Text.Trim();
        //    }, waitInMilliseconds: 1000); 

        //    return messageText;
        //}

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
    }
}
