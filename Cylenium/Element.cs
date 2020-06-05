using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

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

        #region CHARACTERISTICS

        /// <summary>
        /// Get the value of the given attribute.
        /// </summary>
        /// <param name="attrName">The attribute name.</param>
        /// <returns>The attribute value.</returns>
        public string Attribute(string attrName)
        {
            return WebElement.GetAttribute(attrName);
        }

        /// <summary>
        /// Get the value of the javascript property.
        ///
        /// If the value is null or "null", returns null.
        /// If the value is a boolean, returns "True" or "False".
        /// If the value is numeric, returns the string form. For example, 2048 would be "2048".
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value as a string.</returns>
        public string Property(string propertyName)
        {
            return WebElement.GetProperty(propertyName);
        }

        /// <summary>
        /// Get the element tag name.
        /// </summary>
        public string TagName()
        {
            return WebElement.TagName;
        }

        /// <summary>
        /// Get the element text.
        /// </summary>
        public string Text()
        {
            return WebElement.Text;
        }

        #endregion

        #region ACTIONS

        /// <summary>
        /// Check the checkbox or radio element.
        /// </summary>
        /// <returns>The current element.</returns>
        public Element Check()
        {
            var type = WebElement.GetAttribute("type");
            if (type != "checkbox" && type != "radio")
                throw new UnexpectedTagNameException($"Element.Check() expects a 'checkbox' or 'radio' element, but got {type}");

            if (this.IsChecked())
            {
                // do nothing - already checked
            }
            else
            {
                WebElement.Click();
            }
            return this;
        }

        /// <summary>
        /// Clicks the element.
        /// </summary>
        /// <param name="force">Force the click.</param>
        /// <returns>The current element.</returns>
        public Element Click(bool force = false)
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
        /// Double click the element.
        /// </summary>
        /// <returns>The current element.</returns>
        public Element DoubleClick()
        {
            new Actions(cy.WebDriver).MoveToElement(WebElement).DoubleClick().Build().Perform();
            return this;
        }

        /// <summary>
        /// Hover the element.
        /// </summary>
        public Element Hover()
        {
            Actions actions = new Actions(cy.WebDriver);
            actions.MoveToElement(WebElement).Build().Perform();
            return this;
        }

        /// <summary>
        /// Right click (aka Context click) the element.
        /// </summary>
        /// <returns>The current element.</returns>
        public Element RightClick()
        {
            new Actions(cy.WebDriver).MoveToElement(WebElement).ContextClick().Build().Perform();
            return this;
        }

        /// <summary>
        /// Scroll the element into the Viewport.
        /// </summary>
        /// <returns>The current element.</returns>
        public Element ScrollIntoView()
        {
            cy.ExecuteScript<bool>("arguments[0].scrollIntoView(true); return true;", WebElement);
            return this;
        }

        /// <summary>
        /// Selects an &lt;option&gt; element within a &lt;select&gt; dropdown given the text or value.
        /// </summary>
        /// <param name="value">The visible text or value of the option.</param>
        /// <returns>The current dropdown element.</returns>
        public Element Select(string textOrValue)
        {
            if (WebElement.TagName != "select")
                throw new UnexpectedTagNameException($"Element.Select() expects a <select> element but instead got: {WebElement.TagName}");

            var dropdown = new SelectElement(WebElement);
            try
            {
                dropdown.SelectByText(textOrValue);
            }
            catch (NoSuchElementException)
            {
                dropdown.SelectByValue(textOrValue);
            }
            return this;
        }


        /// <summary>
        /// Selects an &lt;option&gt; element within a &lt;select&gt; dropdown given the index.
        /// </summary>
        /// <param name="index">The zero index position of the option.</param>
        /// <returns>The current dropdown element.</returns>
        public Element Select(int index)
        {
            if (WebElement.TagName != "select")
                throw new UnexpectedTagNameException($"Element.Select() expects a <select> element but instead got: {WebElement.TagName}");

            new SelectElement(WebElement).SelectByIndex(index);
            return this;
        }

        /// <summary>
        /// Submit the input or form element.
        /// </summary>
        public Element Submit()
        {
            WebElement.Submit();
            return this;
        }

        /// <summary>
        /// Simulates typing text into the element.
        /// </summary>
        /// <param name="text">The text to type.</param>
        public Element Type(string text)
        {
            WebElement.SendKeys(text);
            return this;
        }

        #endregion

        #region FINDING ELEMENTS

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
        /// Get the siblings of this element.
        /// </summary>
        public Elements Siblings()
        {
            var js = @"
                elem = arguments[0];
                var siblings = [];
                var sibling = elem.parentNode.firstChild;

                while (sibling) {
                    if (sibling.nodeType === 1 && sibling !== elem) {
                        siblings.push(sibling);
                    }
                    sibling = sibling.nextSibling
                }
                return siblings;";
            var elements = cy.ExecuteScript<ReadOnlyCollection<IWebElement>>(js, WebElement);
            return new Elements(null, elements);
        }

        #endregion

        #region CHECKS or EXPECTATIONS

        /// <summary>
        /// A collection of expectations and conditions to check against this element.
        /// </summary>
        /// <param name="timeout">The number of seconds for the condition to be true.</param>
        /// <param name="ignoredExceptions">The list of exceptions to ignore.</param>
        public ElementShould Should(int timeout = -1, params Type[] ignoredExceptions)
        {
            return new ElementShould(this, timeout, ignoredExceptions);
        }

        /// <summary>
        /// Check if the element is checked.
        /// </summary>
        public bool IsChecked() => cy.ExecuteScript<bool>("return arguments[0].checked;", WebElement);

        /// <summary>
        /// Check if the element is displayed.
        /// This means the element is in the DOM and has a size greater than zero.
        /// </summary>
        public bool IsDisplayed() => WebElement.Displayed;

        /// <summary>
        /// Check if the element is enabled.
        /// </summary>
        /// <returns></returns>
        public bool IsEnabled() => WebElement.Enabled;

        /// <summary>
        /// Check if the element is selected.
        /// </summary>
        /// <returns></returns>
        public bool IsSelected() => WebElement.Selected;

        #endregion
    }
}
