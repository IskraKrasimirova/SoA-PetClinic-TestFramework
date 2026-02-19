using OpenQA.Selenium;
using SeleniumFramework.Utilities.Extensions;

namespace SeleniumFramework.Pages
{
    public class FindOwnersPage : BasePage
    {
        private IWebElement FindOwnersHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]//h2"));
        public FindOwnersPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetTitleText()
        {
            return FindOwnersHeader.Text.Trim();
        }

        public void VerifyIsAtFindOwnersPage()
        {
            _driver.WaitUntilUrlContains("owners/find");
            
            Assert.That(FindOwnersHeader.Displayed);
        }
    }
}
