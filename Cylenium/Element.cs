using OpenQA.Selenium;

namespace Cylenium
{
    public class Element
    {
        private Cylenium _cy;
        private IWebElement _webelement;

        public Element(Cylenium cy, IWebElement element)
        {
            _cy = cy;
            _webelement = element;
        }

        /// <summary>
        /// The current instance of WebElement that Element is wrapped around.
        /// </summary>
        public IWebElement WebElement => _webelement;

        /// <summary>
        /// Click on the current element.
        /// </summary>
        /// <returns>The instance of Cylenium.</returns>
        public Cylenium Click()
        {
            WebElement.Click();
            return _cy;
        }
    }
}
