using OpenQA.Selenium;
using PetClinic.UiTests.Utilities;

namespace PetClinic.UiTests.Pages
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
            Retry.Until(() =>
            {
                if (!GreetingHeader.Displayed)
                    throw new RetryException("Home page header not visible yet.");

                if (!PetsImage.Displayed)
                    throw new RetryException("Pets image not visible yet.");

                if (!LogoImage.Displayed)
                    throw new RetryException("Logo image not visible yet.");
            });

            Assert.Multiple(() =>
            {
                Assert.That(GreetingHeader.Displayed, "Welcome header is not visible.");
                Assert.That(GreetingHeader.Text.Trim(), Is.EqualTo("Welcome"));
                Assert.That(PetsImage.Displayed, "Pets image is not visible.");
                Assert.That(LogoImage.Displayed, "Spring logo is not visible.");
            });
        }
    }
}
