using OpenQA.Selenium;

namespace SeleniumTestProject.Actions
{
    public static class CustomMethodsExtension
    {
        public static void ClickElement(this IWebElement element)
        {
            element.Click();
        }

        public static void EnterText(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }
    }
}
