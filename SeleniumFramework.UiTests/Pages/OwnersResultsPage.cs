using OpenQA.Selenium;
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

        public void VerifyOwnerExists(string fullName)
        {
            IWebElement? ownerRow = FindOwnerRowByFullName(fullName);
            Assert.That(ownerRow, Is.Not.Null, $"Owner with fullName {fullName} was not found.");
        }

        public bool IsAtOwnersResultsPage()
        {
            return _driver.Url.Contains("owners?lastName");
        }

        public void VerifyIsAtOwnersResultsPage()
        {
            _driver.WaitUntilUrlContains("owners?lastName");

            Assert.Multiple(() =>
            {
                Assert.That(OwnersHeader.Displayed, "Owners header is not visible.");
                Assert.That(OwnersTable.Displayed, "Owners table is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
            });
        }
    }
}
