using aautomation_framework.Configuration.V3;
using aautomation_framework.Utility;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace aautomation_framework.TestCases
{
    public class BaseTestV3
    {
        protected IWebDriver webBrowser;

        protected BrowserType _browser = BrowserType.Chrome;
        protected string _url = PlatformTestConfiguration.Instance.URL;
        protected string _gateway1 = PlatformTestConfiguration.Instance.Gateway1;
        protected int _commonElementLoadTime = 100;
        protected List<string> _internalAdminUser = new List<string> { "uaint", "Us3r@dmin" };
        protected List<string> _externalAdminUser = new List<string> { "umbrella@external-cust.lan", "Dummy#123" };
        protected List<(string, string)> _regularUsers = new List<(string userName, string userPassword)>
        {
            ( "jack", "Dummy#123" ),
            ( "john@cis-cust.lan", "Dummy#123" )
        };
        protected List<(string, string)> _internalUserData = new List<(string userEmail, string userName)>
        {
            ( "sat_adm3@cis-cust.lan", "sat_adm3" ),
            ( "sat_adm4@cis-cust.lan", "sat_adm4" )
        };
        protected List<(string, string)> _externalUserData = new List<(string userEmail, string userName)>
        {
            ("umbrella_customer@external-cust.lan", "Umbrella GP customer"),
            ("umbrella_ibi@external-cust.lan", "Umbrella ibicp")
        };

        [OneTimeSetUp]
        public void TestSetup()
        {
            LogUtil.CreateLogFile("CommonLogAppenderMinimalLock", PlatformTestConfiguration.Instance.baseResultPath + "\\Regression_UserAdminUI_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".log");
        }

        [SetUp]
        public void RunBeforeTest()
        {

        }

        [TearDown]
        public void TearDown()
        {
            var result = TestContext.CurrentContext.Result;
            if (result.Outcome.Status != TestStatus.Passed)
            {
                LogUtil.log.Error(result.Message);
                LogUtil.log.Error(result.StackTrace);
            }
            webBrowser.Quit();
        }
    }
}
