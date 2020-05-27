using NUnit.Framework;
using OpenQA.Selenium;

namespace Cylenium.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class ElementTests : CySuite
    {
        [Test]
        [Category("element")]
        public void Click_element()
        {
            cy.Visit("https://qap.dev");
            cy.Get("[href='/about']").Click();
            Assert.AreEqual(cy.Title(), "Our Vision — QA at the Point");
        }

        [Test]
        [Category("element")]
        public void Hover_element_to_reveal_menu()
        {
            cy.Visit("https://qap.dev");
            cy.Get("[href='/about']").Hover();
            cy.Contains("Leadership").Click(force: true);
            Assert.That(cy.Contains("Carlos Kidman").IsDisplayed());
        }

        [Test]
        [Category("element")]
        public void Get_element_parent_and_children()
        {
            cy.Visit("https://qap.dev");
            var elements = cy.Get("[href='/our-vision'][data-test]").Parent().Children();
            Assert.AreEqual(3, elements.Count);
        }

        [Test]
        [Category("element")]
        public void Get_element_siblings()
        {
            cy.Visit("https://qap.dev");
            var elements = cy.Get("[href='/our-vision'][data-test]").Siblings();
            Assert.AreEqual(2, elements.Count);
        }

        [Test]
        [Category("element")]
        public void Type_into_field()
        {
            cy.Visit("https://google.com");
            cy.Get("[name='q']").Type("puppies" + Keys.Enter);
            Assert.IsTrue(cy.Title().Contains("puppies"));
        }

        [Test]
        [Category("element")]
        public void Submit_form()
        {
            cy.Visit("http://the-internet.herokuapp.com/login");
            cy.Get("#username").Type("tomsmith");
            cy.Get("#password").Type("SuperSecretPassword!");
            cy.Get("button[type='submit']").Submit();
            cy.Get(".flash.success").IsDisplayed();
        }
    }
}
