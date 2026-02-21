using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumFramework.Utilities.Extensions
{
    public static class WebDriverExtensions
    {
        public static void WaitUntilElementIsClickable(this IWebDriver driver, IWebElement element, int timeoutInSeconds = 5)
        {
            driver.WaitForPredicate(timeoutInSeconds)
                .Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public static void WaitUntilValueIsPresent(this IWebDriver driver, IWebElement element, string value, int timeoutInSeconds = 5)
        {
            driver.WaitForPredicate(timeoutInSeconds)
                .Until(driver => ExpectedConditions.TextToBePresentInElementValue(element, value));
        }

        public static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView()", element);
        }

        public static void ScrollToElementAndClick(this IWebDriver driver, IWebElement element)
        {
            driver.ScrollToElement(element);
            element.Click();
        }

        public static void ScrollToElementAndSendText(this IWebDriver driver, IWebElement element, string text)
        {
            driver.ScrollToElement(element);

            element.Clear();
            element.SendKeys(text);
        }

        public static void WaitUntilUrlContains(this IWebDriver driver, string expectedUrlPart, int timeoutInSeconds = 5)
        {
            driver.WaitForPredicate(timeoutInSeconds)
                .Until(ExpectedConditions.UrlContains(expectedUrlPart));
        }

        public static void EnterText(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public static void EnterDate(this IWebDriver driver, IWebElement element, string date)
        {
            element.Click();

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = arguments[1];", element, date);

            element.SendKeys(Keys.Tab);
        }

        private static WebDriverWait WaitForPredicate(this IWebDriver driver, int timeoutInSeconds = 10)
        {
            var customWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            customWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            
            return customWait;
        }
    }
}
