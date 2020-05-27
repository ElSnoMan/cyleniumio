using NUnit.Framework;

namespace Cylenium
{
    public class CySuite
    {
        [SetUp]
        public virtual void BeforeEach()
        {
            cy.Start();
            cy.Maximize();
        }

        [TearDown]
        public virtual void AfterEach()
        {
            cy.Quit();
        }
    }
}
