using System.Collections.Generic;
using NUnit.Framework;

namespace Cylenium.Tests
{
    public class AssertTests
    {
        [Test]
        public void Assert_contains_item()
        {
            var names = new List<string>() { "carlos", "kidman" };
            Assert.That(names, Contains.Item("carlos"));
        }

        [Test]
        public void Assert_contains_substring()
        {
            var name = "Carlos Enrique Kidman";
            Assert.That(name, Contains.Substring("Kidman"));
        }

        [Test]
        public void Assert_contains_key_and_value()
        {
            var dictionary = new Dictionary<string, string>() {["name"] = "carlos"};
            Assert.That(dictionary, Contains.Key("name"));
            Assert.That(dictionary, Contains.Value("carlos"));
        }

        [Test]
        public void Assert_does_contain()
        {
            var names = new List<string>() { "carlos", "kidman" };
            Assert.That(names, Does.Contain("carlos"));

            var name = "Carlos Enrique Kidman";
            Assert.That(name, Does.Contain("Kidman"));
        }

        [Test]
        public void Assert_does_contain_key_and_value()
        {
            var people = new Dictionary<string, int>() { ["carlos"] = 29, ["Jazzy"] = 9 };
            Assert.That(people, Does.ContainKey("carlos"));
            Assert.That(people, Does.ContainValue(30));
        }

        [Test]
        public void Assert_does_endwith_and_startwith()
        {
            var name = "Carlos Kidman";
            Assert.That(name, Does.EndWith("Kidman"));
            Assert.That(name, Does.StartWith("carlos"));
        }
    }
}
