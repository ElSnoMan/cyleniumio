using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Cylenium
{
    public class ElementShould
    {
        Element _element;
        WebDriverWait _wait;

        public ElementShould(Element element, int timeout = -1, params Type[] ignoredExceptions)
        {
            _element = element;
            _wait = cy.Wait(timeout, ignoredExceptions);
        }

        /// <summary>
        /// An expectation that the element is displayed aka exists in the DOM and is visible.
        /// </summary>
        /// <returns>The current element.</returns>
        public Element BeDisplayed()
        {
            try
            {
                _wait.Until(_ => _element.IsDisplayed());
                return _element;
            }
            catch(WebDriverTimeoutException)
            {
                throw new AssertionException($"Element was not displayed - Locator: ``{_element.By}``");
            }
        }

        /// <summary>
        /// An expectation that the element is checked.
        /// </summary>
        /// <returns>The current element.</returns>
        public Element BeChecked()
        {
            try
            {
                _wait.Until(_ => _element.IsChecked());
                return _element;
            }
            catch(WebDriverTimeoutException)
            {
                throw new AssertionException($"Element was not checked - Locator: ``{_element.By}``");
            }
        }

        /// <summary>
        /// An expectation that the element is clickable aka displayed and enabled.
        /// </summary>
        /// <returns>The current element.</returns>
        public Element BeClickable()
        {
            try
            {
                _wait.Until(_ => _element.IsDisplayed() && _element.IsEnabled());
                return _element;
            }
            catch (WebDriverTimeoutException)
            {
                throw new AssertionException($"Element was not clickable - Locator: ``{_element.By}``");
            }
        }

        /// <summary>
        /// An expectation that the element is disabled.
        /// </summary>
        /// <returns>The current element.</returns>
        public Element BeDisabled()
        {
            try
            {
                _wait.Until(_ => !_element.IsEnabled());
                return _element;
            }
            catch (WebDriverTimeoutException)
            {
                throw new AssertionException($"Element was enabled - Locator: ``{_element.By}``");
            }
        }

        /// <summary>
        /// An expectation that the element is disabled.
        /// </summary>
        /// <returns>The current element.</returns>
        public Element BeEnabled()
        {
            try
            {
                _wait.Until(_ => _element.IsEnabled());
                return _element;
            }
            catch (WebDriverTimeoutException)
            {
                throw new AssertionException($"Element was disabled - Locator: ``{_element.By}``");
            }
        }
    }
}
