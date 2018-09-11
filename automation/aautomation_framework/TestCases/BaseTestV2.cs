using System;
using System.IO;
using aautomation_framework.Configuration.V2;
using aautomation_framework.PageObjects;
using aautomation_framework.Utility;
using NUnit.Framework;
using OpenQA.Selenium;

namespace aautomation_framework.TestCases
{
    public class BaseTestV2
    {
        protected internal IWebDriver webDriver;
        protected static string path = String.Format("{0}{1}{2}{3}{4}", "MC_", DateTime.Now.Month, 
            DateTime.Now.Day, DateTime.Now.Year, ".log");
        private readonly string envUrl = "";
        protected static string tempFolder = Path.Combine(Path.GetTempPath(), "Test_Folder");

        [SetUp]
        public void LogIn()
        {
            LogUtil.CreateLogFile("CommonLogAppender", path);
            WebDriverExtensions.LoadBrowser(ref webDriver, BrowserType.Chrome, tempFolder);
            new LoginPage(webDriver).LoginWithLogs("userName", "userPasswd");
        }

        [Test]
        public void test()
        {
            // v1 choose default env
            string env1 = SettingsV2.Instance.GetEnvUrl();

            // v2 choose another env on local machine
            string env2 = SettingsV2.Instance.GetEnvUrl("ms-qa");

            // v3 setup env veriable for Jenkins 
            Environment.SetEnvironmentVariable("TestStack", "perf");
            string env3 = SettingsV2.Instance.GetEnvUrl("perf");

        }
        
        [TearDown]
        public void LogOut()
        {
            webDriver.Quit();
        }
    }
}
