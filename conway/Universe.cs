using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Conway
{
    public class Universe
    {
        private List<Point> _aliveCells = new List<Point>();

        public List<Point> AliveCells => _aliveCells;

        public void Evolve()
        {
            var aliveToAlive = _aliveCells.Where(c =>
            {
                var aliveNeighborsCount = GetNeighbors(c).Intersect(_aliveCells).Count();
                return aliveNeighborsCount == 2 || aliveNeighborsCount == 3;
            });

            var deadToAlive = _aliveCells.SelectMany(c => GetNeighbors(c).Except(_aliveCells))
                                         .Where(c => c.X >= 0 && c.Y >= 0)
                                         .GroupBy(c => c)
                                         .ToDictionary(c => c.Key, c => c.Count())
                                         .Where(kv => kv.Value == 3)
                                         .Select(kv => kv.Key);

            _aliveCells = aliveToAlive.Union(deadToAlive).ToList();
        }

        private IEnumerable<Point> GetNeighbors(Point p)
        {
            yield return new Point(p.X - 1, p.Y - 1);
            yield return new Point(p.X - 1, p.Y);
            yield return new Point(p.X - 1, p.Y + 1);
            yield return new Point(p.X, p.Y - 1);
            yield return new Point(p.X, p.Y + 1);
            yield return new Point(p.X + 1, p.Y - 1);
            yield return new Point(p.X + 1, p.Y);
            yield return new Point(p.X + 1, p.Y + 1);
        }
    }
}