using OpenQA.Selenium;
using System;

namespace aautomation_framework.PageObjects
{
    public class TemplatePageObject : PageObjectBase
    {
        public TemplatePageObject(IWebDriver driver) : base(driver)
        {
        }

        #region locators
        
        private By SomeLocator1 = By.XPath("//div[@colid = 'submission_type']");
        private By SomeLocator2(string value1, string value2) => By.XPath($"//div[@colid = '{value1}{value2}submission_type']");

        #endregion locators


        public void SomeActionWithElement1()
        {
            WaitForElementIsDisplayed(SomeLocator1).Click();
        }
        
        public void SomeActionWithElement2(string value1, string value2)
        {
            WaitForElementIsDisplayed(SomeLocator2(value1, value2)).Click();
        }

        public void SomeWrapper(string modelFieldValue1, string modelFieldValue2)
        {
            try
            {
                SomeActionWithElement1();
                SomeActionWithElement2(modelFieldValue1, modelFieldValue2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Here will be some logging logic" + ex.Message);
                throw;
            }

        }
    }
}
