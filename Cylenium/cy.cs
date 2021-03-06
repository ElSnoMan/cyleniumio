﻿using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

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
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// The instance of IWebDriver that Cylenium is wrapped around.
        /// </summary>
        public static IWebDriver WebDriver => _driver;

        /// <summary>
        /// Build a WebDriverWait instance given the timeout.
        ///
        /// If timeout = -1 or 0, return default instance of WebDriverWait.
        /// If timeout > 0, return a new instance of WebDriverWait with the given timeout.
        /// </summary>
        /// <param name="timeout">The number of seconds for the set for the Wait.</param>
        /// <param name="ignoreExceptions">The comma-separated list of Exceptions to ignore.</param>
        /// <example>
        /// <code>
        /// // Use default Wait - default timeout is 10 seconds.
        /// cy.Wait().Until(d => d.Title.Contains("QA at the Point"));
        ///
        /// // Wait for 5 seconds
        /// cy.Wait(5).Until(d => d.Url.Contains("qap.dev"));
        /// // --or--
        /// cy.Wait(timeout: 5).Until(_ => cy.Url().Contains("qap.dev"));
        /// </code>
        /// </example>
        public static WebDriverWait Wait(int timeout = -1, params Type[] ignoreExceptions)
        {
            var wait = _wait;

            if (timeout <= 0)
            {
                // use default _wait
            }
            else if (timeout >= 1)
            {
                wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeout));
                wait.IgnoreExceptionTypes(ignoreExceptions);
            }
            else
            {
                throw new ArgumentException($"timeout override must be a positive integer, but was {timeout}");
            }

            return wait;
        }

        #region CHARACTERISTICS

        /// <summary>
        /// Get the current page title.
        /// </summary>
        public static string Title()
        {
            return WebDriver.Title;
        }

        /// <summary>
        /// Get the current URL.
        /// </summary>
        public static string Url()
        {
            return WebDriver.Url;
        }

        #endregion

        #region ACTIONS

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
        /// Quits the current driver.
        /// </summary>
        public static void Quit()
        {
            WebDriver.Quit();
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
        /// Navigate forward or backward "N" number of pages.
        /// </summary>
        /// <param direction="forward" or "back">Go 'forward' or 'backward.</param>
        /// <param number="1" default is 1>Number of pages to go forwards or backwards.</param>
        public static void Go(string direction, int number = 1)
        {
            if (direction == "backward")
            {
                cy.ExecuteScript<String>("window.history.go(arguments[0])", number * -1);
            }
            else if (direction == "forward")
            {
                cy.ExecuteScript<String>("window.history.go(arguments[0])", number);
            }
            else
            {
                throw new ArgumentException($"direction was invalid. Must be `forward` or `back` but was {direction}");
            }
        }

        #endregion

        #region FINDING ELEMENTS

        /// <summary>
        /// Find the element that contains the given text.
        /// </summary>
        /// <param name="text">The text for the element to contain.</param>
        /// <param timeout=-1>The max number of seconds to wait for the element to be found.</param>
        /// <returns>The Element when found.</returns>
        public static Element Contains(string text, int timeout=-1)
        {
            var by = By.XPath($"//*[contains(text(), '{text}')]");

            if (timeout == 0)
            {
                return new Element(by, WebDriver.FindElement(by));
            }

            var element = Wait(timeout).Until(drvr => drvr.FindElement(by));
            return new Element(by, element);
        }

        /// <summary>
        /// Find the elements with the given CSS selector.
        /// </summary>
        /// <param name="css">The CSS selector.</param>
        /// <param name="atLeastOne">Wait until at least one is found?</param>
        /// <param timeout=-1>The max number of seconds to wait for an element to be found.</param>
        /// <returns>The list of Elements</returns>
        public static Elements Find(string css, bool atLeastOne = true, int timeout = -1)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            var by = By.CssSelector(css);

            if (atLeastOne)
            {
                Wait(timeout).Until(drvr => drvr.FindElements(by).Count > 0);
                elements = WebDriver.FindElements(by);
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
        /// <param timeout=-1>The max number of seconds to wait for an element to be found.</param>
        /// <returns>The list of Elements.</returns>
        public static Elements FindX(string xpath, bool atLeastOne = false, int timeout = -1)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            var by = By.XPath(xpath);

            if (atLeastOne)
            {
                Wait(timeout).Until(drvr => drvr.FindElements(by).Count > 0);
                elements = WebDriver.FindElements(by);
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
        /// <param timeout=-1>The max number of seconds to wait for the element to be found.</param>
        /// <returns>The Element when found.</returns>
        public static Element Get(string css, int timeout = -1)
        {
            var by = By.CssSelector(css);
            if (timeout == 0)
            {
                return new Element(by, WebDriver.FindElement(by));
            }
            var element = Wait(timeout).Until(drvr => drvr.FindElement(by));
            return new Element(by, element);
        }

        /// <summary>
        /// Find the element with the given Xpath.
        /// </summary>
        /// <param name="xpath">The Xpath selector.</param>
        /// <param timeout=-1>The max number of seconds to wait for the element to be found.</param>
        /// <returns>The Element when found.</returns>
        public static Element GetX(string xpath, int timeout = -1)
        {
            var by = By.XPath(xpath);
            if (timeout == 0)
            {
                return new Element(by, WebDriver.FindElement(by));
            }
            var element = Wait(timeout).Until(drvr => drvr.FindElement(by));
            return new Element(by, element);
        }

        #endregion

        #region UTILITIES

        /// <summary>
        /// Take a screenshot of the current window.
        /// </summary>
        /// <param name="fileName">The file name including the path and extension.</param>
        public static void Screenshot(string fileName)
        {
            var ss = ((ITakesScreenshot)WebDriver).GetScreenshot();
            ss.SaveAsFile(fileName, ScreenshotImageFormat.Png);
        }

        #endregion

        #region BROWSER

        /// <summary>
        /// Adds a cookie to the current page.
        /// </summary>
        /// <returns>The cookie that was added.</returns>
        public static Cookie AddCookie(Cookie cookie)
        {
            WebDriver.Manage().Cookies.AddCookie(cookie);
            return cookie;
        }

        /// <summary>
        /// Deletes the cookie with the given name.
        /// </summary>
        public static void DeleteCookie(string name)
        {
            WebDriver.Manage().Cookies.DeleteCookieNamed(name);
        }

        /// <summary>
        /// Deletes all cookies from the current page.
        /// </summary>
        public static void DeleteCookies()
        {
            WebDriver.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        /// Gets the cookie with the given name.
        /// </summary>
        /// <returns>Returns Null if the cookie is not found.</returns>
        public static Cookie GetCookie(string name)
        {
            return WebDriver.Manage().Cookies.GetCookieNamed(name);
        }

        /// <summary>
        /// Gets all cookies from the current page.
        /// </summary>
        public static ReadOnlyCollection<Cookie> GetCookies()
        {
            return WebDriver.Manage().Cookies.AllCookies;
        }

        /// <summary>
        /// Gets the number of window handles (This could be for windows or tabs.)
        /// </summary>
        /// <returns>A read only collection of window handle IDs(strings).</returns>
        public static ReadOnlyCollection<string> WindowHandles()
        {
            return WebDriver.WindowHandles;
        }

        /// <summary>
        /// Maximizes the current window.
        /// </summary>
        public static void Maximize()
        {
            WebDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Gets the size of the current window.
        /// </summary>
        /// <returns>Size of the current window</returns>
        public static Size WindowSize()
        {
            return WebDriver.Manage().Window.Size;
        }

        /// <summary>
        /// Set the size of the current window given the width and height.
        /// </summary>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <returns>The new Size of the current window.</returns>
        public static Size WindowSize(int width, int height)
        {
            WebDriver.Manage().Window.Size = new Size(width, height);
            return WindowSize();
        }

        #endregion
    }
}
