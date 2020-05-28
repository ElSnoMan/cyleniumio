using System;
using NUnit.Framework;

namespace Cylenium.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class DriverTests : CySuite
    {
        [Test]
        [Category("Driver"), Category("Wait")]
        public void Customize_wait_timeout_builds_new_wait()
        {
            var wait = cy.Wait(5);

            cy.Visit("https://google.com");
            wait.Until(_ => cy.Get("[name='q']"));

            Assert.AreEqual(5, wait.Timeout.Seconds);
        }

        [Test]
        [Category("Driver"), Category("Wait")]
        public void Customize_wait_with_negative_timeout_uses_default()
        {
            var wait = cy.Wait(timeout: -5);

            cy.Visit("https://google.com");
            wait.Until(_ => cy.Get("[name='q']"));

            Assert.AreEqual(10, wait.Timeout.Seconds);
        }
    }
}
