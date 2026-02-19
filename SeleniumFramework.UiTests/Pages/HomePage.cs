using OpenQA.Selenium;

namespace SeleniumFramework.Pages
{
    public class HomePage : BasePage
    {
        private IWebElement GreetingHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]//h2"));
        private IWebElement PetsImage => _driver.FindElement(By.XPath("//img[contains(@src,'pets')]"));
        private IWebElement LogoImage => _driver.FindElement(By.XPath("//img[contains(@src,'spring')]"));

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public void VerifyIsAtHomePage()
        {
            Assert.That(_driver.Url, Does.Contain("8080"));

            Assert.Multiple(() =>
            {
                Assert.That(GreetingHeader.Displayed, "Welcome header is not visible.");
                Assert.That(PetsImage.Displayed, "Pets image is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
            });
        }
    }
}
