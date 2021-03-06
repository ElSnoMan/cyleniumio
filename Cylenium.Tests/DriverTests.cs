using System;
using NUnit.Framework;
using OpenQA.Selenium;

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
        public void Navigate_backward_and_forward()
        {
            cy.Visit("https://google.com");
            var google_title = cy.Title();

            cy.Visit("https://www.ultimateqa.com");
            var ultimateqa_title = cy.Title();

            cy.Go("backward", 1);
            Assert.AreEqual(google_title, cy.Title(), "Expected to be on " + google_title + " went backward to" + cy.Title());

            cy.Go("forward", 1);
            Assert.AreEqual(ultimateqa_title, cy.Title(), "Expected to be on" + ultimateqa_title + " went forward to" + cy.Title());
        }

        [Test]
        [Category("Driver")]
        public void Count_number_of_tabs()
        {
            cy.Visit("https://google.com");
            cy.ExecuteScript<String>("window.open('https://www.qap.dev/', '_blank')");
            Assert.AreEqual(2, cy.WindowHandles().Count);
        }

        [Test]
        [Category("Driver")]
        public void Delete_all_cookies()
        {
            cy.Visit("https://google.com");
            cy.AddCookie(new Cookie("foo", "bar"));
            cy.DeleteCookies();
            Assert.AreEqual(0, cy.GetCookies().Count);
        }

        [Test]
        [Category("Driver")]
        public void Get_and_delete_cookie()
        {
            cy.Visit("https://google.com");
            var cookieAdded = cy.AddCookie(new Cookie("foo", "bar"));
            var cookie = cy.GetCookie("foo");
            Assert.AreEqual(cookieAdded, cookie);

            cy.DeleteCookie(cookie.Name);
            Assert.IsNull(cy.GetCookie("foo"));
        }
    }
}