using OpenQA.Selenium;

namespace SeleniumFramework.Pages
{
    public class HomePage : BasePage
    {
        private IWebElement HomeLink => _driver.FindElement(By.XPath("//a[@title='home page']"));
        private IWebElement FindOwnersLink => _driver.FindElement(By.XPath("//a[@href='/owners/find']"));
        private IWebElement VeterinariansLink => _driver.FindElement(By.XPath("//a[@title='veterinarians']"));
        private IWebElement ErrorLink => _driver.FindElement(By.XPath("//a[@href='/oups']"));
        private IWebElement GreetingHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]//h2"));
        private IWebElement PetsImage => _driver.FindElement(By.XPath("//img[contains(@src,'pets')]"));
        private IWebElement LogoImage => _driver.FindElement(By.XPath("//img[contains(@src,'spring')]"));

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateToFindOwnersPage()
        {
            FindOwnersLink.Click();
        }

        public void VerifyIsAtHomePage()
        {
            Assert.That(_driver.Url, Does.Contain("8080"));

            Assert.Multiple(() =>
            {
                Assert.That(HomeLink.Displayed, "Home link is not visible.");
                Assert.That(FindOwnersLink.Displayed, "FindOwners link is not visible.");
                Assert.That(VeterinariansLink.Displayed, "Veterinarians link is not visible.");
                Assert.That(ErrorLink.Displayed, "Error link is not visible.");
                Assert.That(GreetingHeader.Displayed, "Welcome header is not visible.");
                Assert.That(PetsImage.Displayed, "Pets image is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
            });
        }
    }
}
