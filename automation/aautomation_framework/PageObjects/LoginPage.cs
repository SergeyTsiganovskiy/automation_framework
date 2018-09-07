using aautomation_framework.Configuration;
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
    }
}
