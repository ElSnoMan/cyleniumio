using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Cylenium
{
    public class cy
    {
        [ThreadStatic]
        private static IWebDriver _driver;

        [ThreadStatic]
        private static WebDriverWait _wait;

        public static void Start()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// The instance of IWebDriver that Cylenim is wrapped around.
        /// </summary>
        public static IWebDriver WebDriver => _driver;

# region FINDING ELEMENTS

        /// <summary>
        /// Find the element that contains the given text.
        /// </summary>
        /// <param name="text">The text for the element to contain.</param>
        /// <returns>The Element when found.</returns>
        public static Element Contains(string text)
        {
            var by = By.XPath($"//*[contains(text(), '{text}')]");
            var element = _wait.Until(drvr => drvr.FindElement(by));
            return new Element(by, element);
        }

        /// <summary>
        /// Find the elements with the given CSS selector.
        /// </summary>
        /// <param name="css">The CSS selector.</param>
        /// <param name="atLeastOne">Wait until at least one is found?</param>
        /// <returns>The list of Elements</returns>
        public static Elements Find(string css, bool atLeastOne=false)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            var by = By.CssSelector(css);

            if (atLeastOne)
            {
                elements = _wait.Until(drvr => drvr.FindElements(by));
            }
            else
            {
                elements = WebDriver.FindElements(by);
            }

            return new Elements(by, elements);
        }

        /// <summary>
        /// Find the elements with the given XPath.
        /// </summary>
        /// <param name="xpath">The XPath.</param>
        /// <param name="atLeastOne">Wait until at least one is found?</param>
        /// <returns>The list of Elements.</returns>
        public static Elements FindX(string xpath, bool atLeastOne=false)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            var by = By.XPath(xpath);

            if (atLeastOne)
            {
                elements = _wait.Until(drvr => drvr.FindElements(by));
            }
            else
            {
                elements = WebDriver.FindElements(by);
            }

            return new Elements(by, elements);
        }

        /// <summary>
        /// Find the element with the given CSS selector.
        /// </summary>
        /// <param name="css">The CSS selector.</param>
        /// <returns>The Element when found.</returns>
        public static Element Get(string css)
        {
            var by = By.CssSelector(css);
            var element = _wait.Until(drvr => drvr.FindElement(by));
            return new Element(by, element);
        }

        /// <summary>
        /// Find the element with the given Xpath.
        /// </summary>
        /// <param name="xpath">The Xpath selector.</param>
        /// <returns>The Element when found.</returns>
        public static Element Xpath(string xpath)
        {
            var by = By.XPath(xpath);
            var element = _wait.Until(drvr => drvr.FindElement(by));
            return new Element(by, element);
        }

# endregion

        /// <summary>
        /// Executes javascript into the current window.
        /// </summary>
        /// <param name="javascript">The js string to execute.</param>
        /// <typeparam name="T">The type you expect to get back.</typeparam>
        /// <returns>The value returned by the javascript.</returns>
        public static T ExecuteScript<T>(string javascript, params object[] args)
        {
            var jse = (IJavaScriptExecutor)_driver;
            return (T)jse.ExecuteScript(javascript, args);
        }

        /// <summary>
        /// Get the current page title.
        /// </summary>
        public static string Title()
        {
            return WebDriver.Title;
        }

        /// <summary>
        /// Maximizes the current window.
        /// </summary>
        public static void Maximize()
        {
            WebDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        public static void Visit(string url)
        {
            WebDriver.Url = url;
        }

        /// <summary>
        /// Get the current URL.
        /// </summary>
        public static string Url()
        {
            return WebDriver.Url;
        }

        /// <summary>
        /// Quits the current driver.
        /// </summary>
        public static void Quit()
        {
            WebDriver.Quit();
        }
    }
}
