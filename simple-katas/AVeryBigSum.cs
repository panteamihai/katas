using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SimpleKatas
{
    //https://www.hackerrank.com/challenges/a-very-big-sum/problem
    public class AVeryBigSum
    {
        private static IEnumerable<TestCaseData> PowersTestCases
        {
            get
            {
                yield return new TestCaseData(0, new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                yield return new TestCaseData(1, new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 });
                yield return new TestCaseData(10, new[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 });
                yield return new TestCaseData(103, new[] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 3 });
                yield return new TestCaseData(5346, new[] { 0, 0, 0, 0, 0, 0, 5, 3, 4, 6 });
            }
        }

        [TestCaseSource(nameof(PowersTestCases))]
        public void SplitTest(long number, int[] powers)
        {
            Assert.AreEqual(powers, TurnToPowersOfTen(number));
        }

        [TestCaseSource(nameof(PowersTestCases))]
        public void MergeTest(long number, int[] powers)
        {
            Assert.AreEqual(number, GetNumberBase10(powers));
        }

        private static int[] TurnToPowersOfTen(long number)
        {
            var powers = new int[10];
            var i = 0;

            do
            {
                powers[i++] = (int)(number % 10);
                number = number / 10;
            } while (number > 0);

            return powers.Reverse().ToArray();
        }

        private static long GetNumberBase10(IEnumerable<int> powers)
        {
            return powers.Aggregate<int, long>(0, (acc, p) => acc * 10 + p);
        }

        private static long AVeryBigSum2(long[] ar)
        {
            return GetNumberBase10(ar.Select(TurnToPowersOfTen).Aggregate(new int[11], (accumulator, currentNumberPowers) => {
                var carryOver = 0;
                int i;
                for (i = currentNumberPowers.Length - 1; i >= 0; i--)
                {
                    var pow = accumulator[i + 1] + currentNumberPowers[i] + carryOver;
                    accumulator[i + 1] = pow % 10;
                    carryOver = pow / 10;
                }

                if (carryOver > 0)
                    accumulator[0] = carryOver;

                return accumulator;
            }));
        }
    }
}
