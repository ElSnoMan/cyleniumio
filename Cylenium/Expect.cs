using NUnit.Framework;

namespace Cylenium
{
    public class Expect
    {
        // var checkbox;
        // Expect.That(checkbox).IsChecked();
        // Expect.That(checkbox).Should().BeChecked();
        // checkbox.Should().BeChecked();

        public static void That(Element condition)
        {
            if (condition == null || condition.GetType() != typeof(Element))
            {
                throw new AssertionException("Should() expectation failed. Element is null or invalid.");
            }
        }
    }
}
