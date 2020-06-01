using System.Collections.Generic;
using NUnit.Framework;

namespace Cylenium.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class Challenges : CySuite
    {
        [Test]
        [Category("Challenge")]
        public void STG_Challenge_3()
        {
            /*
            1. Go to copart.com
            2. Gather the popular Makes and Models on the page
            3. Print the name and url of each Make or Model
            4. Check that there are 20 Makes and Models
            */
            cy.Visit("https://copart.com");
            var makesModels = cy.Find("[ng-repeat*='popularSearch'] > a");

            foreach (var car in makesModels)
            {
                var name = car.Text();
                var url = car.Attribute("href");
                System.Console.WriteLine($"{name} - {url}");
            }

            Assert.That(makesModels.Count, Is.EqualTo(20));
        }

        [Test]
        [Category("Challenge")]
        public void STG_Challenge_7()
        {
            /*
            1. Go to copart.com
            2. Look at the Makes/Models section of the page
            3. Create a two-dimensional list that stores the names of the Make/Model as well as their URLs
            4. Check that each element in this list navigates to the correct page
            */
            cy.Visit("https://copart.com");

            var cars = new List<List<string>>();
            foreach (var make in cy.Find("[ng-repeat*='popularSearch'] > a"))
            {
                var name = make.Text();
                var url = make.Attribute("href");
                cars.Add(new List<string>() {name, url});
            }

            foreach (var car in cars)
            {
                var name = car[0];
                var url = car[1];

                cy.Visit(url);
                Assert.IsTrue(cy.Contains(name.ToLower()).IsDisplayed());
            }
        }
    }
}
