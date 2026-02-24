using OpenQA.Selenium;
using PetClinic.UiTests.Models;
using PetClinic.UiTests.Utilities;
using PetClinic.UiTests.Utilities.Extensions;

namespace PetClinic.UiTests.Pages
{
    public class OwnerDetailsPage : BasePage
    {
        private IWebElement OwnerInfoHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]//h2"));
        private IWebElement OwnerInfoTable => _driver.FindElement(By.XPath("//table[.//th[text()='Name']]"));
        private IWebElement EditOwnerButton => _driver.FindElement(By.XPath("//a[text()='Edit Owner']"));
        private IWebElement AddPetButton => _driver.FindElement(By.XPath("//a[text()='Add New Pet']"));
        private IWebElement LogoImage => _driver.FindElement(By.XPath("//img[contains(@src,'spring')]"));
        private IWebElement PetsAndVisitsHeader => _driver.FindElement(By.XPath("//h2[@id='petsAndVisits']"));
        private IWebElement NameValueCell => _driver.FindElement(By.XPath("//th[text()='Name']/following-sibling::td"));
        private IWebElement AddressValueCell => _driver.FindElement(By.XPath("//th[text()='Address']/following-sibling::td"));
        private IWebElement CityValueCell => _driver.FindElement(By.XPath("//th[text()='City']/following-sibling::td"));
        private IWebElement TelephoneValueCell => _driver.FindElement(By.XPath("//th[text()='Telephone']/following-sibling::td"));
        private IReadOnlyCollection<IWebElement> AddVisitLinks => _driver.FindElements(By.XPath("//a[contains(text(),'Add Visit')]"));
        private IWebElement? FindPetSectionByName(string petName) => _driver.FindElements(By.XPath("//dl"))
           .FirstOrDefault(dl => dl.Text.Contains(petName));
        private IWebElement? FindVisitRowByDescription(string description) => 
            _driver.FindElements(By.XPath("//table[.//th[text()='Visit Date']]//tbody/tr"))
           .FirstOrDefault(r => r.Text.Contains(description));

        public OwnerDetailsPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsAtOwnerDetailsPage()
        {
            return _driver.Url.Contains("owners/");
        }

        public void NavigateToAddNewPet()
        {
            AddPetButton.Click();
        }

        public void ClickAddVisitForTheFirstPet()
        {
            if (AddVisitLinks.Count == 0)
            {
                throw new Exception("This owner has no pets and no visits can be added.");
            }

            AddVisitLinks.First().Click();
        }

        public void VerifyOwnerDetails(OwnerModel model)
        {
            Assert.Multiple(() =>
            {
                Assert.That(NameValueCell.Text, Is.EqualTo($"{model.FirstName} {model.LastName}"));
                Assert.That(AddressValueCell.Text, Is.EqualTo(model.Address));
                Assert.That(CityValueCell.Text, Is.EqualTo(model.City));
                Assert.That(TelephoneValueCell.Text, Is.EqualTo(model.Telephone));
            });
        }

        public void VerifyPetDetails(PetModel pet)
        {
            var petSection = FindPetSectionByName(pet.Name);

            Assert.That(petSection, Is.Not.Null, $"Pet '{pet.Name}' was not found.");

            var name = petSection.FindElement(By.XPath(".//dt[text()='Name']/following-sibling::dd[1]")).Text.Trim();
            var birthDate = petSection.FindElement(By.XPath(".//dt[text()='Birth Date']/following-sibling::dd[1]")).Text.Trim();
            var type = petSection.FindElement(By.XPath(".//dt[text()='Type']/following-sibling::dd[1]")).Text.Trim();

            Assert.Multiple(() =>
            {
                Assert.That(name, Is.EqualTo(pet.Name));
                Assert.That(birthDate, Is.EqualTo(pet.BirthDate));
                Assert.That(type, Is.EqualTo(pet.Type));
            });
        }

        public void VerifyVisitExists(string visitDescription)
        {
            var visitRow = FindVisitRowByDescription(visitDescription);

            Assert.That(visitRow, Is.Not.Null, $"Visit with description '{visitDescription}' was not found.");
        }

        public void VerifyIsAtOwnerDetailsPage()
        {
            _driver.WaitUntilUrlContains("owners/");

            Retry.Until(() =>
            {
                if (!OwnerInfoTable.Displayed)
                    throw new RetryException("Owner Details page not loaded yet.");
            });

            Assert.Multiple(() =>
            {
                Assert.That(OwnerInfoHeader.Displayed, "Owner Information header is not visible.");
                Assert.That(OwnerInfoHeader.Text.Trim(), Is.EqualTo("Owner Information"));
                Assert.That(OwnerInfoTable.Displayed, "OwnerInfo table is not visible.");
                Assert.That(EditOwnerButton.Displayed, "EditOwner button is not visible.");
                Assert.That(AddPetButton.Displayed, "AddPet button is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
                Assert.That(PetsAndVisitsHeader.Displayed, "PetsAndVisits header is not visible.");
            });
        }
    }
}
