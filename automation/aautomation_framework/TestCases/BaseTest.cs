using aautomation_framework.Utility;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace aautomation_framework.TestCases
{
    public abstract class BaseTestV1
    {
        private IWebDriver _webDriver;

        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            InitBrowser();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _webDriver.CloseBrowser();
        }


        protected T GetPage<T>() where T : class
        {
            return Activator.CreateInstance(typeof(T), _webDriver) as T;
        }

        private void InitBrowser()
        {
            WebDriverExtensions.LoadBrowser(ref _webDriver, BrowserType.Chrome, string.Empty);
        }
    }
}
