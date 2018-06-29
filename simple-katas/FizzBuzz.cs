using NUnit.Framework;

namespace SimpleKatas
{
    public class FizzBuzz
    {
        public bool IsDivisibleByThree(int value)
        {
            return value % 3 == 0;
        }

        public bool IsDivisibleByFive(int value)
        {
            return value % 5 == 0;
        }

        public string GetTextForNumber(int value)
        {
            var output = string.Empty;

            if (IsDivisibleByThree(value))
                output += "Fizz";

            if (IsDivisibleByFive(value))
                output += "Buzz";

            return string.IsNullOrEmpty(output) ? value.ToString() : output;
        }

        public string CanDoTheWholeThing(int valueUpTo)
        {
            var output = string.Empty;

            for (var i = 1; i <= valueUpTo; i++)
            {
                output += GetTextForNumber(i) + " ";
            }

            return output.Trim();
        }
    }

    [TestFixture]
    public class FizzBuzzTests
    {
        private FizzBuzz _fb;

        [SetUp]
        public void Setup()
        {
            _fb = new FizzBuzz();
        }

        [Test]
        public void ThreeIsDivisibleByThree()
        {
            var divisibleByThree = _fb.IsDivisibleByThree(3);

            Assert.IsTrue(divisibleByThree);
        }

        [Test]
        public void FiveIsDivisibleByFive()
        {
            var divisibleByFive = _fb.IsDivisibleByFive(5);

            Assert.IsTrue(divisibleByFive);
        }

        [Test]
        public void FifteenIsDivisibleByThree_AndDivisibleByFive()
        {
            var divisibleByThree = _fb.IsDivisibleByThree(15);
            var divisibleByFive = _fb.IsDivisibleByFive(15);

            Assert.IsTrue(divisibleByThree && divisibleByFive);
        }

        [Test]
        public void TextForNumberThreeIsFizz()
        {
            var text = _fb.GetTextForNumber(3);

            Assert.AreEqual("Fizz", text);
        }

        [Test]
        public void TextForNumberFiveIsBuzz()
        {
            var text = _fb.GetTextForNumber(5);

            Assert.AreEqual("Buzz", text);
        }

        [Test]
        public void TextForNumberOneIsDigitOne()
        {
            var text = _fb.GetTextForNumber(1);

            Assert.AreEqual("1", text);
        }

        [Test]
        public void TextForNumberFifteenIsFizzBuzz()
        {
            var text = _fb.GetTextForNumber(15);

            Assert.AreEqual("FizzBuzz", text);
        }

        [Test]
        public void CanDoTheWholeThing()
        {
            var text = _fb.CanDoTheWholeThing(15);

            Assert.That(text, Is.EqualTo("1 2 Fizz 4 Buzz Fizz 7 8 Fizz Buzz 11 Fizz 13 14 FizzBuzz"));
        }

        [TearDown]
        public void Teardown()
        {
            _fb = null;
        }
    }
}
