using OpenQA.Selenium;
using Reqnroll;
using SeleniumFramework.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Steps
{
    [Binding]
    public class FindOwnersSteps
    {
        private readonly IWebDriver _driver;
        private readonly FindOwnersPage _findOwnersPage;

        public FindOwnersSteps(IWebDriver driver, FindOwnersPage findOwnersPage)
        {
            this._driver = driver;
            this._findOwnersPage = findOwnersPage;
        }

        [Then("the {string} title is displayed")]
        public void ThenTheTitleIsDisplayed(string expectedTitle)
        {
            _findOwnersPage.VerifyIsAtFindOwnersPage();

            var actualTitle = _findOwnersPage.GetTitleText();
            Assert.That(actualTitle, Is.EqualTo(expectedTitle));
        }
    }
}
