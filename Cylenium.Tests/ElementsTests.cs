using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Cylenium.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class ElementsTests : CySuite
    {
        [Test]
        [Category("elements"), Category("element")]
        public void Find_elements_by_css()
        {
            cy.Visit("http://the-internet.herokuapp.com/add_remove_elements/");
            cy.Contains("Add Element").Click().Click().Click().Click().Click();
            Assert.That(cy.Find("[onclick='deleteElement()']").Count, Is.EqualTo(5));
        }

        [Test]
        [Category("elements"), Category("element")]
        public void Find_elements_by_xpath()
        {
            cy.Visit("http://the-internet.herokuapp.com/add_remove_elements/");
            cy.Contains("Add Element").Click().Click().Click().Click().Click();
            Assert.That(cy.Find("//*[@onclick='deleteElement()']").Count, Is.EqualTo(5));
        }

        [Test]
        [Category("elements"), Category("element")]
        public void Find_elements_within_element_by_css()
        {
            cy.Visit("http://the-internet.herokuapp.com/add_remove_elements/");
            cy.Contains("Add Element").Click().Click().Click().Click().Click();
            var elements = cy.Get("#elements").Find("button");
            Assert.That(elements.Count, Is.EqualTo(5));
        }

        [Test]
        [Category("elements"), Category("element")]
        public void Find_elements_within_element_by_xpath()
        {
            cy.Visit("http://the-internet.herokuapp.com/add_remove_elements/");
            cy.Contains("Add Element").Click().Click().Click().Click().Click();
            var elements = cy.Get("#elements").FindX(".//button");
            Assert.That(elements.Count, Is.EqualTo(5));
        }
    }
}
