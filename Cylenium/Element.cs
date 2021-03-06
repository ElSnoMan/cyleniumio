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

            if (!this.IsChecked())
            {
                WebElement.Click();
            }
            return this;
        }

        /// <summary>
        /// Uncheck the checkbox or radio element.
        /// </summary>
        /// <returns>The current element.</returns>
        public Element Uncheck()
        {
            var type = WebElement.GetAttribute("type");
            if (type != "checkbox" && type != "radio")
                throw new UnexpectedTagNameException($"Element.Uncheck() expects a 'checkbox' or 'radio' element, but got {type}");

            if (this.IsChecked())
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
        /// Deselects an &lt;option&gt; element within a &lt;select&gt; dropdown given the text or value.
        ///
        /// Only works with multi-option dropdowns.
        /// </summary>
        /// <param name="value">The visible text or value of the option.</param>
        /// <returns>The current dropdown element.</returns>
        public Element Deselect(string textOrValue)
        {
            if (WebElement.TagName != "select")
                throw new UnexpectedTagNameException($"Element.Select() expects a <select> element but instead got: {WebElement.TagName}");

            var dropdown = new SelectElement(WebElement);
            try
            {
                dropdown.DeselectByText(textOrValue);
            }
            catch (NoSuchElementException)
            {
                dropdown.DeselectByValue(textOrValue);
            }
            return this;
        }


        /// <summary>
        /// Deselects an &lt;option&gt; element within a &lt;select&gt; dropdown given the index.
        ///
        /// Only works with multi-option dropdowns.
        /// </summary>
        /// <param name="index">The zero index position of the option.</param>
        /// <returns>The current dropdown element.</returns>
        public Element Deselect(int index)
        {
            if (WebElement.TagName != "select")
                throw new UnexpectedTagNameException($"Element.Select() expects a <select> element but instead got: {WebElement.TagName}");

            new SelectElement(WebElement).DeselectByIndex(index);
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
        /// Find the element that contains the given text.
        /// </summary>
        /// <param name="text">The text for the element to contain.</param>
        /// <param timeout=-1>The max number of seconds to wait for the element to be found.</param>
        /// <returns>The Element when found.</returns>
        public Element Contains(string text, int timeout = -1)
        {
            var by = By.XPath($".//*[contains(text(), '{text}')]");

            if (timeout == 0)
            {
                return new Element(by, WebElement.FindElement(by));
            }

            var element = cy.Wait(timeout).Until(_ => WebElement.FindElement(by));
            return new Element(by, element);
        }

        /// <summary>
        /// Find the elements with the given CSS selector.
        /// </summary>
        /// <param name="css">The CSS selector.</param>
        /// <param name="atLeastOne">Wait until at least one is found?</param>
        /// <param timeout=-1>The max number of seconds to wait for an element to be found.</param>
        /// <returns>The list of Elements</returns>
        public Elements Find(string css, bool atLeastOne = true, int timeout = -1)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            var by = By.CssSelector(css);

            if (atLeastOne)
            {
                cy.Wait(timeout).Until(_ => WebElement.FindElements(by).Count > 0);
                elements = WebElement.FindElements(by);
            }
            else
            {
                elements = WebElement.FindElements(by);
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
        public Elements FindX(string xpath, bool atLeastOne = false, int timeout = -1)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            var by = By.XPath(xpath);

            if (atLeastOne)
            {
                cy.Wait(timeout).Until(_ => WebElement.FindElements(by).Count > 0);
                elements = WebElement.FindElements(by);
            }
            else
            {
                elements = WebElement.FindElements(by);
            }

            return new Elements(by, elements);
        }

        /// <summary>
        /// Find the element with the given CSS selector.
        /// </summary>
        /// <param name="css">The CSS selector.</param>
        /// <param timeout=-1>The max number of seconds to wait for the element to be found.</param>
        /// <returns>The Element when found.</returns>
        public Element Get(string css, int timeout = -1)
        {
            var by = By.CssSelector(css);
            if (timeout == 0)
            {
                return new Element(by, WebElement.FindElement(by));
            }
            var element = cy.Wait(timeout).Until(_ => WebElement.FindElement(by));
            return new Element(by, element);
        }

        /// <summary>
        /// Find the element with the given Xpath.
        /// </summary>
        /// <param name="xpath">The Xpath selector.</param>
        /// <param timeout=-1>The max number of seconds to wait for the element to be found.</param>
        /// <returns>The Element when found.</returns>
        public Element GetX(string xpath, int timeout = -1)
        {
            var by = By.XPath(xpath);
            if (timeout == 0)
            {
                return new Element(by, WebElement.FindElement(by));
            }
            var element = cy.Wait(timeout).Until(_ => WebElement.FindElement(by));
            return new Element(by, element);
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
