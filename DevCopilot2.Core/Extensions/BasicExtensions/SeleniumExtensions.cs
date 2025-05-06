using OpenQA.Selenium;

namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class SeleniumExtensions
    {
        public static bool ElementExists(this IWebDriver driver,
            string cssSelector, out IWebElement? element, int elementIndex = 0)
        {
            try
            {
                var elements = driver.FindElements(By.CssSelector(cssSelector));
                if (elements.Count -1 >= elementIndex)
                    element = elements[elementIndex];
                else
                    element = elements[0];
                return true;
            }
            catch (Exception ex)
            {
                element = null;
                return false;
            }
        }
    }
}
