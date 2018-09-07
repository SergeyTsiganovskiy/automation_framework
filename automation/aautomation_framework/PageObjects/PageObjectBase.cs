using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace aautomation_framework.PageObjects
{
    public class PageObjectBase
    {
        private IWebDriver _webDriver;

        public PageObjectBase(IWebDriver driver)
        {

        }

        public IWebDriver WebDriver { get => _webDriver; set => _webDriver = value; }

        protected IWebElement WaitForElementIsDisplayed(By locator, int timeout = 30)
        {
            return new WebDriverWait(_webDriver, TimeSpan.FromSeconds(timeout)).Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        protected IWebElement WaitForElementIsClickable(By locator, int timeout = 30)
        {
            return new WebDriverWait(_webDriver, TimeSpan.FromSeconds(timeout)).Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }
    }
}
