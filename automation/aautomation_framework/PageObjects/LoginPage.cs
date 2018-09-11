using System;
using aautomation_framework.Configuration;
using aautomation_framework.Configuration.V1;
using aautomation_framework.Utility;
using OpenQA.Selenium;

namespace aautomation_framework.PageObjects
{
    public class LoginPage : PageObjectBase
    {
        private By userNameLocator = By.XPath("");
        private By passwordLocator = By.XPath("");
        private By loginButtonLocator = By.XPath("");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public void Navigate()
        {
            // logout
            WebDriver.Manage().Cookies.DeleteCookieNamed("usersession");
            WebDriver.Manage().Cookies.DeleteCookieNamed("userDetails");
            WebDriver.Navigate().GoToUrl("Get from setting");
        }

        public void LoginAsManager()
        {
            Login(SettingsV1.Manager.UserName, SettingsV1.Manager.Password);
        }

        private void Login(string user, string password)
        {
            WaitForElementIsDisplayed(userNameLocator).SendKeys(user);
            WaitForElementIsDisplayed(passwordLocator).SendKeys(password);
            WaitForElementIsClickable(loginButtonLocator).Click();
        }

        public void LoginWithLogs(string username, string password)
        {
            if (username != null && password != null)
            {
                WebDriverExtensions.GetElementByWithLogs(WebDriver, By.XPath("//*[@id='username']"), "Can not find input field username").SendKeys(username);
                LogUtil.WriteDebug("Can find input field username and type this value: " + username);
                WebDriverExtensions.GetElementByWithLogs(WebDriver, By.XPath("//input[@ng-model='ctrl.password']"), "Can not find input field password").SendKeys(password);
                LogUtil.WriteDebug("Can find input field password and type this value: " + password);
                WebDriverExtensions.GetElementByWithLogs(WebDriver, By.XPath("//span[contains(.,'Sign In')]"), "Can not find button Login").Click();
                LogUtil.WriteDebug("Can find button Login and click on it");
            }
            else
            {
                NullReferenceException argEx = new NullReferenceException("Credentials cannot be null");
            }
        }
    }
}
