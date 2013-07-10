using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Life5
{
    public class Game : LifeGameLibrary
    {
        public const int Height = 50;
        public const int Width = 50;

        public Game(HashSet<Size> hashSet)
            : base(hashSet)
        {
        }

        public String CellToString(Size cell)
        {
            return AliveCells.Contains(cell) ? "#" : " ";
        }

        public String RowToString(int rowNumber)
        {
            return Enumerable.Range(0, Width)
                             .Select(p => CellToString(new Size(p, rowNumber)))
                             .Aggregate((a, b) => a + b);
        }

        public override string ToString()
        {
            return Enumerable.Range(0, Height).Reverse()
                             .Select(RowToString)
                             .Aggregate((a, b) => a + "\n" + b);
        }

    }
}
