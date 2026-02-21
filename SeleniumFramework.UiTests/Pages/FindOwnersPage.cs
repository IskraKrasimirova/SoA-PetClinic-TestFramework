using OpenQA.Selenium;
using SeleniumFramework.Utilities.Extensions;

namespace SeleniumFramework.Pages
{
    public class FindOwnersPage : BasePage
    {
        private IWebElement FindOwnersHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]//h2"));
        private IWebElement LastNameInput => _driver.FindElement(By.XPath("//input[@id='lastName']"));
        private IWebElement FindOwnerButton => _driver.FindElement(By.XPath("//button[text()='Find Owner']"));
        private IWebElement AddOwnerButton => _driver.FindElement(By.XPath("//a[text()='Add Owner']"));
        private IWebElement LogoImage => _driver.FindElement(By.XPath("//img[contains(@src,'spring')]"));
        private IWebElement OwnersTable => _driver.FindElement(By.XPath("//table[@id='ownersTable']"));

        public FindOwnersPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetTitleText()
        {
            return FindOwnersHeader.Text.Trim();
        }

        public void NavigateToAddOwnerPage()
        {
            AddOwnerButton.Click();
        }

        internal void NavigateToOwnersResultsPage()
        {
            FindOwnerButton.Click();
        }

        public void SearchByLastName(string lastName)
        {
            LastNameInput.EnterText(lastName);
            FindOwnerButton.Click();
        }

        public void VerifyIsAtFindOwnersPage()
        {
            _driver.WaitUntilUrlContains("owners/find");

            Assert.Multiple(() =>
            {
                Assert.That(FindOwnersHeader.Displayed, "FindOwners header is not visible.");
                Assert.That(LastNameInput.Displayed, "Last Name field is not visible.");
                Assert.That(FindOwnerButton.Displayed, "FindOwner button is not visible.");
                Assert.That(AddOwnerButton.Displayed, "AddOwner button is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
            });
        }
    }
}
