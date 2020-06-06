using NUnit.Framework;

namespace Cylenium.Tests
{
    public class Jane : CySuite
    {
        [Test]
        public void AddItemToCart()
        {
            cy.Visit("https://jane.com");
            cy.Get("[data-testid='deal-image']").Click();

            var dropdowns = cy.Find("select");
            foreach (var dropdown in dropdowns)
            {
                dropdown.Select(1);
            }
            cy.Get("[data-testid='add-to-bag']").Click();
            cy.Get("[data-testid='cart-grand-total']").Should().BeDisplayed();
        }
    }
}
