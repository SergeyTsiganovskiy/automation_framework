using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;

namespace aautomation_framework.Utility
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Ie,
        Edge,
        Safari
    }

    public static class WebDriverExtensions
    {

        public static void LoadBrowser(ref IWebDriver driver, BrowserType browserType, string downloadPath)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--start-miximized");
                    options.AddArguments("--disable-extensions", "--disable-infobars");
                    if (!string.IsNullOrEmpty(downloadPath))
                    {
                        options.AddUserProfilePreference("download.default_directory", downloadPath);
                    }

                    //Disable popup for password saving

                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);
                    options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);

                    var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    driver = new ChromeDriver(path, options);
                    break;
                case BrowserType.Firefox:
                    driver = new FirefoxDriver();
                    break;
                case BrowserType.Ie:
                    InternetExplorerOptions internetExplorerOptions = new InternetExplorerOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Eager,
                        UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
                    };
                    path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    driver = new InternetExplorerDriver(path, internetExplorerOptions);
                    break;
                case BrowserType.Edge:
                    driver = new EdgeDriver();
                    break;
                case BrowserType.Safari:
                    driver = new SafariDriver();
                    break;
            }
        }


        public static object ExecuteJavaScript(this IWebDriver driver, string script)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script);
        }

        public static void WaitUntilPageIsLoaded(this IWebDriver driver, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(d =>
                ((IJavaScriptExecutor) driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void ClickOnElementByAction(this IWebDriver driver, IWebElement element)
        {
            var action = new Actions(driver);
            action.MoveToElement(element).Click();
            action.Perform();
        }

        /// <summary>
        /// Close the broswer and driver instrances
        /// </summary>
        /// <param name="webBrowser"></param>
        public static void CloseBrowser(this IWebDriver webBrowser)
        {
            try
            {
                webBrowser.Close();
            }
            finally
            {
                webBrowser.Quit();
            }
        }
    }
}
