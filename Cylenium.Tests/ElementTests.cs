using NUnit.Framework;
using OpenQA.Selenium;

namespace Cylenium.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class ElementTests : CySuite
    {
        [Test]
        [Category("element")]
        public void ClickElement()
        {
            cy.Visit("https://qap.dev");
            cy.Get("[href='/about']").Click();
            Assert.AreEqual(cy.Title(), "Our Vision — QA at the Point");
        }

        [Test]
        [Category("element")]
        public void HoverElement()
        {
            cy.Visit("https://qap.dev");
            cy.Get("[href='/about']").Hover();
            cy.Contains("Leadership").Click(force: true);
            Assert.That(cy.Contains("Carlos Kidman").IsDisplayed());
        }

        [Test]
        [Category("element")]
        public void ParentAndChildren()
        {
            cy.Visit("https://qap.dev");
            var elements = cy.Get("[href='/our-vision'][data-test]").Parent().Children();
            Assert.AreEqual(3, elements.Count);
        }

        [Test]
        [Category("element")]
        public void Siblings()
        {
            cy.Visit("https://qap.dev");
            var elements = cy.Get("[href='/our-vision'][data-test]").Siblings();
            Assert.AreEqual(2, elements.Count);
        }

        [Test]
        [Category("element")]
        public void TypeIntoElement()
        {
            cy.Visit("https://google.com");
            cy.Get("[name='q']").Type("puppies" + Keys.Enter);
            Assert.IsTrue(cy.Title().Contains("puppies"));
        }

        [Test]
        [Category("element")]
        public void SubmitElement()
        {
            cy.Visit("http://the-internet.herokuapp.com/login");
            cy.Get("#username").Type("tomsmith");
            cy.Get("#password").Type("SuperSecretPassword!");
            cy.Get("button[type='submit']").Submit();
            cy.Get(".flash.success").IsDisplayed();
        }
    }
}
