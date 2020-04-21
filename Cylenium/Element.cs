using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Cylenium
{
    public class Element
    {
        private By _by;

        private IWebElement _webelement;

        public Element(By by, IWebElement element)
        {
            _by = by;
            _webelement = element;
        }

        /// <summary>
        /// The By locator used to find this element.
        /// </summary>
        public By By => _by;

        /// <summary>
        /// The current instance of WebElement that Element is wrapped around.
        /// </summary>
        public IWebElement WebElement => _webelement;

        /// <summary>
        /// Clicks the element.
        /// </summary>
        /// <param name="force">Force the click.</param>
        /// <returns>The current element.</returns>
        public Element Click(bool force=false)
        {
            if (force)
            {
                cy.ExecuteScript<bool>("arguments[0].click(); return true;", WebElement);
            }
            else
            {
                WebElement.Click();
            }

            return this;
        }

        /// <summary>
        /// Get the value of the given attribute.
        /// </summary>
        /// <param name="attrName">The attribute name.</param>
        /// <returns>The attribute value.</returns>
        public string GetAttr(string attrName)
        {
            return WebElement.GetAttribute(attrName);
        }

        /// <summary>
        /// Get the children of this element.
        /// </summary>
        public Elements Children()
        {
            var elements = cy.ExecuteScript<ReadOnlyCollection<IWebElement>>("return arguments[0].children;", WebElement);
            return new Elements(_by, elements);
        }

        /// <summary>
        /// Get the parent of this element.
        /// </summary>
        public Element Parent()
        {
            var element = cy.ExecuteScript<IWebElement>("return arguments[0].parentElement;", WebElement);
            return new Element(_by, element);
        }

        /// <summary>
        /// Hover the element.
        /// </summary>
        public Element Hover()
        {
            Actions actions = new Actions(cy.WebDriver);
            actions.MoveToElement(WebElement).Perform();
            return this;
        }

        /// <summary>
        /// Check if the element is displayed.
        /// This means the element is in the DOM and has a size greater than zero.
        /// </summary>
        public bool IsDisplayed() => WebElement.Displayed;
    }
}
