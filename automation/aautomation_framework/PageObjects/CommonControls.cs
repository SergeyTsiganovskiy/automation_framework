using OpenQA.Selenium;

namespace aautomation_framework.PageObjects
{
    public class CommonControls : PageObjectBase
    {
        public CommonControls(IWebDriver driver) : base(driver)
        {

        }

        public void RefreshPage()
        {
            WebDriver.Navigate().Refresh();
        }

        public void NavigateBack()
        {
            WebDriver.Navigate().Back();
        }

        public string GetCurrentUrl()
        {
            return WebDriver.Url;
        }

        public static void ExpandGridWidth(IWebDriver webDriver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("document.getElementById('center').style.width = '1000000000px';");
        }

        public void LogOut()
        {
            WaitForElementIsClickable(null).Click();
            WaitForElementIsClickable(null).Click();
        }
    }
}
