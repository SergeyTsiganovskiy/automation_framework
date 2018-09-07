using NUnit.Framework;
using System;

namespace aautomation_framework.Utility
{
    //NUnit Categories which are common across all applications
    #region CommonCategroies
    /// <summary>
    /// NUnit Attribute to identify all the tests which are included in the Full Regression test cycle
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SmokeTestsAttribute : CategoryAttribute
    { }

    /// <summary>
    /// NUnit Attribute to identify all the tests which are included in the Snoke Tests. These are the base or core regression tests
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FullRegressionAttribute : CategoryAttribute
    { }

    /// <summary>
    /// NUnit Attribute to identify all the tests which are included in the Performance. This test set includes mostly JMeter/API tests
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PerformanceAttribute : CategoryAttribute
    { }

    #endregion CommonCategories
}
