using OpenQA.Selenium;

namespace PetClinic.UiTests.Pages
{
    public class NavigationBar : BasePage
    {
        private IWebElement BrandLink => _driver.FindElement(By.XPath("//a[@class='navbar-brand']"));
        private IWebElement HomeLink => _driver.FindElement(By.XPath("//a[@title='home page']"));
        private IWebElement FindOwnersLink => _driver.FindElement(By.XPath("//a[@href='/owners/find']"));
        private IWebElement VeterinariansLink => _driver.FindElement(By.XPath("//a[@title='veterinarians']"));
        private IWebElement ErrorLink => _driver.FindElement(By.XPath("//a[@href='/oups']"));

        public NavigationBar(IWebDriver driver) : base(driver)
        {
        }

        public void GoToHomePage() => HomeLink.Click(); 
        public void GoToFindOwnersPage() => FindOwnersLink.Click();
        public void GoToVeterinariansPage() => VeterinariansLink.Click(); 
        public void GoToErrorPage() => ErrorLink.Click();

        public void VerifyNavigationIsVisible()
        {
            Assert.Multiple(() =>
            {
                Assert.That(BrandLink.Displayed, "Brand link is not visible.");
                Assert.That(HomeLink.Displayed, "Home link is not visible.");
                Assert.That(FindOwnersLink.Displayed, "Find Owners link is not visible.");
                Assert.That(VeterinariansLink.Displayed, "Veterinarians link is not visible.");
                Assert.That(ErrorLink.Displayed, "Error link is not visible.");
            });
        }
    }
}
