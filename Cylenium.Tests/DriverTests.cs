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

            Assert.That(wait.Timeout.Seconds, Is.EqualTo(5));
        }

        [Test]
        [Category("Driver"), Category("Wait")]
        public void Customize_wait_with_negative_timeout_uses_default()
        {
            var wait = cy.Wait(timeout: -5);

            cy.Visit("https://google.com");
            wait.Until(_ => cy.Get("[name='q']"));

            Assert.That(wait.Timeout.Seconds, Is.EqualTo(10));
        }


        [Test]
        [Category("Driver")]
        public void Set_and_get_the_window_size()
        {
            var expectedSize = cy.WindowSize(width: 800, height: 600);
            Assert.AreEqual(cy.WindowSize(), expectedSize);
        }

        [Test]
        [Category("Driver")]
        public void Count_number_of_tabs()
        {
            var wait = cy.Wait();
            string test_url = "https://www.qap.dev/";

            cy.Visit("https://google.co");
            cy.ExecuteScript<String>("window.open('" + test_url + "', '_blank')");
            Assert.AreEqual(2, cy.WindowHandles().Count);
        }
    }
}
