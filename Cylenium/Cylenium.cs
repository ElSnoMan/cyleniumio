using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Cylenium
{
    public class Cylenium
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public Cylenium()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// The instance of IWebDriver that Cylenim is wrapped around.
        /// </summary>
        public IWebDriver WebDriver => _driver;

# region FINDING ELEMENTS

        /// <summary>
        /// Find the element that contains the given text.
        /// </summary>
        /// <param name="text">The text for the element to contain.</param>
        /// <returns></returns>
        public Element Contains(string text)
        {
            var element = _wait.Until(drvr => drvr.FindElement(By.XPath($"//*[contains(text(), {text})]")));
            return new Element(this, element);
        }

        /// <summary>
        /// Find the elements with the given CSS selector.
        /// </summary>
        /// <param name="css">The CSS selector.</param>
        /// <returns></returns>
        public IList<IWebElement> Find(string css)
        {
            return _wait.Until(drvr => drvr.FindElements(By.CssSelector(css)));
        }

        /// <summary>
        /// Find the element with the given CSS selector.
        /// </summary>
        /// <param name="css">The CSS selector.</param>
        /// <returns></returns>
        public Element Get(string css)
        {
            var element = _wait.Until(drvr => drvr.FindElement(By.CssSelector(css)));
            return new Element(this, element);
        }

        /// <summary>
        /// Find the element with the given Xpath.
        /// </summary>
        /// <param name="xpath">The Xpath selector.</param>
        /// <returns></returns>
        public Element Xpath(string xpath)
        {
            var element = _wait.Until(drvr => drvr.FindElement(By.XPath(xpath)));
            return new Element(this, element);
        }

# endregion

        /// <summary>
        /// Get the current page's title.
        /// </summary>
        public string Title()
        {
            return WebDriver.Title;
        }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        /// <returns>The instance of Cylenium to chain another command.</returns>
        public Cylenium Visit(string url)
        {
            WebDriver.Url = url;
            return this;
        }

        /// <summary>
        /// Get the current URL.
        /// </summary>
        public string Url()
        {
            return WebDriver.Url;
        }

        /// <summary>
        /// Quits the current driver.
        /// </summary>
        public void Quit()
        {
            WebDriver.Quit();
        }
    }

    public class CyleniumTests
    {
        public Cylenium cy;

        [SetUp]
        public virtual void Setup()
        {
            cy = new Cylenium();
        }

        [TearDown]
        public virtual void TearDown()
        {
            cy.Quit();
        }
    }
}
