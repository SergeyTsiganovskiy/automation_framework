using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

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
                    if (!String.IsNullOrEmpty(downloadPath))
                    {
                        options.AddUserProfilePreference("download.default_directory", downloadPath);
                    }

                    //Disable popup for password saving

                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);
                    options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);

                    var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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
                    path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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

        public static IWebElement GetElementByWithLogs(IWebDriver driver, By by, string message, int second = 10)
        {
            IWebElement element = null;
            if (@by != null)
            {

                try
                {
                    element = WaitForElementIsVisible(driver, @by, second);
                    var Y = element.Location.Y - 150;
                    driver.ExecuteJavaScript("scroll(0," + Y + ")");
                }
                catch (NoSuchElementException e)
                {

                    throw e;

                }
                catch (Exception e)
                {
                    LogUtil.WriteDebug("I've been waiting 10 seconds element but it is not visible");
                    LogUtil.WriteDebug(e.ToString() + '\n' + '\n' + "**************************" + message + "**************************" + '\n' + @by);
                    TakeScreenshot(driver);
                    throw e;
                }
            }
            return element;
        }

        public static IWebElement WaitForElementIsVisible(IWebDriver driver, By locator, int maxsecond)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxsecond)).Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static void TakeScreenshot(IWebDriver driver)
        {
            string currentpath = TestContext.CurrentContext.TestDirectory;
            string[] pathList = currentpath.Split('\\');
            string sequence = @"\";
            string subPath = "ImagesPath";
            bool exists = System.IO.Directory.Exists(pathList[0] + sequence + subPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(pathList[0] + sequence + subPath);
            string path = pathList[0] + sequence + subPath + sequence;
            string PublicScreenPath = "from settings";
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(path + timestamp + ".jpeg", ScreenshotImageFormat.Jpeg);
            LogUtil.WriteDebug("\nPlease find more information on this screen here: " + PublicScreenPath + timestamp + ".jpeg");
        }
    }
}
