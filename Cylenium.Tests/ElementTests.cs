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

        [Test]
        [Category("element")]
        public void Select_valid_text_option_from_dropdown()
        {
            cy.Visit("http://the-internet.herokuapp.com/dropdown");
            cy.Get("#dropdown").Select("Option 2");
            Assert.AreEqual(cy.Contains("Option 2").Attribute("selected"), "true");
        }

        [Test]
        [Category("element")]
        public void Select_invalid_text_option_but_valid_value_option_from_dropdown()
        {
            cy.Visit("http://the-internet.herokuapp.com/dropdown");
            cy.Get("#dropdown").Select("2");
            Assert.AreEqual(cy.Contains("Option 2").Attribute("selected"), "true");
        }

        [Test]
        [Category("element")]
        public void Select_option_from_dropdown_by_index()
        {
            cy.Visit("http://the-internet.herokuapp.com/dropdown");
            Assert.IsFalse(cy.Contains("Option 2").IsSelected());

            cy.Get("#dropdown").Select(2);
            Assert.IsTrue(cy.Contains("Option 2").IsSelected());
        }

        [Test]
        [Category("element")]
        public void Right_click_element_to_reveal_context_menu()
        {
            cy.Visit("http://the-internet.herokuapp.com/context_menu");
            cy.Get("#hot-spot").RightClick();
            cy.Wait().Until(drvr => drvr.SwitchTo().Alert()).Accept();
        }
    }
}
