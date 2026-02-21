using OpenQA.Selenium;
using SeleniumFramework.Models;
using SeleniumFramework.Utilities.Extensions;

namespace SeleniumFramework.Pages
{
    public class OwnersResultsPage : BasePage
    {
        private IWebElement OwnersHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]//h2"));
        private IWebElement OwnersTable => _driver.FindElement(By.XPath("//table[@id='ownersTable']"));
        private IWebElement LogoImage => _driver.FindElement(By.XPath("//img[contains(@src,'spring')]"));
        private IReadOnlyCollection<IWebElement> OwnerRows => _driver.FindElements(By.XPath("//table[@id='ownersTable']/tbody/tr"));
        private IWebElement? FindOwnerRowByFullName(string fullName) =>
            _driver.FindElements(By.XPath($"//table[@id='ownersTable']//td[text()='{fullName}']/parent::tr"))
           .FirstOrDefault();


        public OwnersResultsPage(IWebDriver driver) : base(driver)
        {
        }

        public void SelectFirstOwner()
        {
            Assert.That(OwnerRows, Is.Not.Empty, "No owners are found in the results table.");

            var firstOwnerLink = OwnerRows.First().FindElement(By.TagName("a"));
            firstOwnerLink.Click();
        }

        public void VerifyOwnerExists(OwnerModel expectedOwner)
        {
            var fullName = $"{expectedOwner.FirstName} {expectedOwner.LastName}";
            IWebElement? ownerRow = FindOwnerRowByFullName(fullName);

            Assert.That(ownerRow, Is.Not.Null, $"Owner with fullName {fullName} was not found.");

            var headers = OwnersTable.FindElements(By.XPath(".//thead/tr/th"))
                .Select(th => th.Text.Trim())
                .ToList();

            var expectedHeaders = new List<string> { "Name", "Address", "City", "Telephone", "Pets" };
            Assert.That(headers, Is.EqualTo(expectedHeaders), "Table headers do not match expected values.");

            var cells = ownerRow.FindElements(By.XPath("./td"))
                .Select(td => td.Text.Trim())
                .ToList();

            Assert.That(cells[0], Is.EqualTo(fullName), "Owner's full name does not match.");

            Assert.Multiple(() =>
            {
                Assert.That(cells[1], Is.EqualTo(expectedOwner.Address), "Owner's address does not match.");
                Assert.That(cells[2], Is.EqualTo(expectedOwner.City), "Owner's city does not match.");
                Assert.That(cells[3], Is.EqualTo(expectedOwner.Telephone), "Owner's telephone does not match.");
            });
        }

        public bool IsAtOwnersResultsPage()
        {
            return _driver.Url.Contains("owners?lastName=");
        }

        public void VerifyIsAtOwnersResultsPage()
        {
            _driver.WaitUntilUrlContains("owners?lastName=");

            Assert.Multiple(() =>
            {
                Assert.That(OwnersHeader.Displayed, "Owners header is not visible.");
                Assert.That(OwnersHeader.Text.Trim(), Is.EqualTo("Owners"));
                Assert.That(OwnersTable.Displayed, "Owners table is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
            });
        }
    }
}
