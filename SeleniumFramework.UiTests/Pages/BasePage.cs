using OpenQA.Selenium;

namespace SeleniumFramework.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver _driver;

        protected IWebElement BaseElement => _driver.FindElement(By.TagName("body"));

        public BasePage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void VerifyPageIsLoaded()
        {
            Assert.That(BaseElement.Displayed, Is.True);
        }
    }
}
