using System.Collections.Generic;
using aautomation_framework.PageObjects;
using aautomation_framework.Utility;
using NUnit.Framework;

namespace aautomation_framework.TestCases.Functional
{
    public class TemplateTestV1 : BaseTestV1
    {
        private LoginPage loginPage;

        public void OneTimeSetUp()
        {
            loginPage = GetPage<LoginPage>();
            loginPage.Navigate();
            loginPage.LoginAsManager();
        }

        [Test, FullRegression, Order(1)]
        public void Ex00000_ToDo_Smth()
        {
            // some actions
            Assert.That("", Is.EqualTo(""), $"Cannot perform an action. Message: '{"some info"} {"some info"}'");
            CollectionAssert.AreEquivalent(null, null, "Actual specialties do not represent the expected from the database");
            foreach (var risk in new List<TestDelegate>())
            {
                Assert.That(risk, Is.GreaterThanOrEqualTo("").And.LessThanOrEqualTo(""), 
                    "The cases are not filtered by risk score range");
            }

            StringAssert.Contains("", "", "Search result item does not contain searching text");

            if (true)
            {
                // Fails the test case and continues to the next flow 
                Assert.Fail();
            };
        }
    }
}
