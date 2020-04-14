using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Cylenium
{
    public class Cylenium
    {
        private IWebDriver _driver;

        public Cylenium()
        {
            _driver = new ChromeDriver();
        }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        /// <returns>The instance of Cylenium to chain another command.</returns>
        public Cylenium Visit(string url)
        {
            _driver.Url = url;
            return this;
        }

        /// <summary>
        /// Quits the current driver.
        /// </summary>
        public void Quit()
        {
            _driver.Quit();
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
