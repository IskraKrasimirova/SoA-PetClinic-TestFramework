using OpenQA.Selenium;
using SeleniumFramework.Models;
using SeleniumFramework.Utilities.Extensions;

namespace SeleniumFramework.Pages
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
        private IWebElement PetsTable => _driver.FindElement(By.XPath("//table[.//th[text()='Pet Name']]"));
        private IWebElement VisitsTable => _driver.FindElement(By.XPath("//table[.//th[text()='Visit Date']]"));


        public OwnerDetailsPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsAtOwnerDetailsPage()
        {
            return _driver.Url.Contains("owners/");
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

        public void VerifyIsAtOwnerDetailsPage()
        {
            _driver.WaitUntilUrlContains("owners/");

            Assert.Multiple(() =>
            {
                Assert.That(OwnerInfoHeader.Displayed, "Owner Information header is not visible.");
                Assert.That(OwnerInfoTable.Displayed, "OwnerInfo table is not visible.");
                Assert.That(EditOwnerButton.Displayed, "EditOwner button is not visible.");
                Assert.That(AddPetButton.Displayed, "AddPet button is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
                Assert.That(PetsAndVisitsHeader.Displayed, "PetsAndVisits header is not visible.");
            });
        }
    }
}
