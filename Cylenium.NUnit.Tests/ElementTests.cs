using Cylenium;
using NUnit.Framework;

namespace Tests
{
    public class ElementTests : CyleniumTests
    {
        [Test]
        public void ClickElement()
        {
            cy.Visit("https://qap.dev");
            var title = cy.Get("[href='/about']").Click().Title();
            Assert.AreEqual(title, "Our Vision — QA at the Point");
        }
    }
}
