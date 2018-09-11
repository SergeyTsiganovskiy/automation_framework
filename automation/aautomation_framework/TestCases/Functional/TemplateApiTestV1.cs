using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aautomation_framework.Utility;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace aautomation_framework.TestCases.Functional
{
    [TestFixture]
    public class TemplateApiTestV1
    {
        protected static readonly string envUrl = "Is getting from config file";
        protected readonly string userName = "Is getting from config file";
        protected readonly string userPassword = "Is getting from config file";
        protected static readonly int port = 8080;
        protected static readonly ApiCallsClient api = new ApiCallsClient(envUrl, port);


        [SetUp]
        public void SetUp()
        {
            api.StartSession(userName, userPassword);
        }

        [Test]
        public void TestMethod()
        {

        }
    }
}
