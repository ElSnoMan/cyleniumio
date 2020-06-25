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
        public void Go_forward_and_backward()
        {
            var wait = cy.Wait();
            var base_url = cy.Visit("https://google.com");
            wait.Until(_ => cy.Get("[name='q']"));
            cy.Visit("https://www.qap.dev/");
            cy.Get("[href='/leadership']").Click();
            wait.Until(_ => cy.Get("[href='http://alarm.com/']"));
            cy.Go("back", 2);
        public void Count_number_of_tabs()
        {
            cy.Visit("https://google.com");
            cy.ExecuteScript<String>("window.open('https://www.qap.dev/', '_blank')");
            Assert.AreEqual(2, cy.WindowHandles().Count);
        }
    }
}
