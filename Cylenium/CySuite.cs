using NUnit.Framework;
using NUnit.Framework.Interfaces;

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
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                // this currently goes to Cylenium.Tests > bin folder
                cy.Screenshot("test_failed.png");
            }

            cy.Quit();
        }
    }
}
