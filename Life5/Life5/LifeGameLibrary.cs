using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Life5
{
    public class LifeGameLibrary
    {
        protected readonly HashSet<Size> AliveCells;
        public readonly List<Size> Offset = new List<Size>(new[]
            {
                new Size(-1, 1), new Size(0, 1), 
                new Size(1, 1), new Size(-1, 0), 
                new Size(1, 0), new Size(-1, -1), 
                new Size(0, -1), new Size(1, -1)
            });

        public LifeGameLibrary(HashSet<Size> hashSet)
        {
            AliveCells = hashSet;
        }

        public IEnumerable<Size> AllAdjecent(Size point)
        {
            return Offset.Select(n => n + point);
        }

        public IEnumerable<Size> EnumerateAlive(Size point)
        {
            return AllAdjecent(point).Where(n => AliveCells.Contains(n));
        }

        public bool WillBeAlive(Size point)
        {
            int count = EnumerateAlive(point).Count();
            return count == 3 || (count == 2 && AliveCells.Contains(point));
        }

        public HashSet<Size> NextStep()
        {
            return new HashSet<Size>(
                    AliveCells.SelectMany(AllAdjecent).Where(WillBeAlive)
                );
        }
    }
}
