using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace Conway
{
    [TestFixture]
    public class UniverseTests
    {
        [Test]
        public void GivenEmptyUniverse_WhenEvolving_WillStillBeEmpty()
        {
            var universe = new Universe();

            universe.Evolve();

            Assert.That(universe.AliveCells, Is.Empty);
        }

        [Test]
        public void GivenUniverseWithAliveCellsWithNoNighbors_WhenEvolving_WillStillBeEmpty()
        {
            var universe = new Universe();
            universe.AliveCells.Add(new Point(0, 0));
            universe.AliveCells.Add(new Point(2, 1));
            universe.AliveCells.Add(new Point(13, 7));

            universe.Evolve();

            Assert.That(universe.AliveCells, Is.Empty);
        }

        [Test]
        public void GivenUniverseWithAliveCellsWithSingleAliveNeighbors_WhenEvolving_WillStillBeEmpty()
        {
            var universe = new Universe();
            universe.AliveCells.Add(new Point(0, 0));
            universe.AliveCells.Add(new Point(0, 1));
            universe.AliveCells.Add(new Point(7, 1));
            universe.AliveCells.Add(new Point(7, 2));
            universe.AliveCells.Add(new Point(13, 7));
            universe.AliveCells.Add(new Point(12, 8));

            universe.Evolve();

            Assert.That(universe.AliveCells, Is.Empty);
        }


        [Test]
        public void GivenUniverseWithOneAliveCellWithTwoNonAdjoiningAliveNeighbors_WhenEvolving_WillContainThatSingleCell()
        {
            var universe = new Universe();
            universe.AliveCells.Add(new Point(2, 0));
            universe.AliveCells.Add(new Point(1, 1));
            universe.AliveCells.Add(new Point(0, 2));

            universe.Evolve();

            Assert.That(universe.AliveCells.Single(), Is.EqualTo(new Point(1, 1)));
        }

        [Test]
        public void GivenUniverseWithOneAliveCellWithTwoAdjoiningAliveNeighbors_WhenEvolving_WillContainAllThreeCells()
        {
            var universe = new Universe();
            universe.AliveCells.Add(new Point(0, 0));
            universe.AliveCells.Add(new Point(1, 0));
            universe.AliveCells.Add(new Point(0, 1));

            universe.Evolve();

            Assert.That(universe.AliveCells, Contains.Item(new Point(0, 0)));
            Assert.That(universe.AliveCells, Contains.Item(new Point(0, 1)));
            Assert.That(universe.AliveCells, Contains.Item(new Point(1, 0)));
        }

        [Test]
        public void GivenUniverseWithOneDeadCellWithThreeAliveNeighbors_WhenEvolving_WillContainAliveCellAtPreviouslyDeadCellsPosition()
        {
            var universe = new Universe();
            universe.AliveCells.Add(new Point(0, 0));
            universe.AliveCells.Add(new Point(1, 0));
            universe.AliveCells.Add(new Point(0, 1));

            universe.Evolve();

            Assert.That(universe.AliveCells, Contains.Item(new Point(1, 1)));
        }

        [Test]
        public void GivenUniverseWithOneDeadCellOutisdeTheBoundriesWithThreeAliveNeighbors_WhenEvolving_WillNotContainAliveCellAtPreviouslyDeadCellsPosition()
        {
            var universe = new Universe();
            universe.AliveCells.Add(new Point(0, 0));
            universe.AliveCells.Add(new Point(0, 1));
            universe.AliveCells.Add(new Point(0, 2));

            universe.Evolve();

            Assert.That(universe.AliveCells, Does.Not.Contain(new Point(-1, 1)));
        }

        [Test]
        public void GivenUniverseWithBlinkerInPhase1_WhenEvolving_WillBlinkerInPhase2()
        {
            var universe = new Universe();
            universe.AliveCells.Add(new Point(0, 1));
            universe.AliveCells.Add(new Point(1, 1));
            universe.AliveCells.Add(new Point(2, 1));

            universe.Evolve();

            Assert.That(universe.AliveCells, Contains.Item(new Point(1, 0)));
            Assert.That(universe.AliveCells, Contains.Item(new Point(1, 1)));
            Assert.That(universe.AliveCells, Contains.Item(new Point(1, 2)));
        }
    }
}
