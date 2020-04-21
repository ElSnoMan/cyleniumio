using NUnit.Framework;

namespace Cylenium.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class ElementTests : CyTests
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
    }
}
