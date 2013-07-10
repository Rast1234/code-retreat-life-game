using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life1
{
    class Program
    {
        static void Main(string[] args)
        {
            Field f = Field.GenerateRandomField(10, 10);
            while (true)
            {
                Console.WriteLine(f.ToString());
                Field nextField = f.nextStep();
                f = nextField;
                System.Threading.Thread.Sleep(500);
            }
        }
    }
    class Cell
    {
        public bool Alive { get; set; }
        public override string ToString()
        {
            return Alive ? "#" : ".";
        }
        public Cell(bool alive)
        {
            Alive = alive;
        }
    }
    class Field
    {
        private int height, width;
        private List<List<Cell>> grid;
        public Field(int height, int width)
        {
            this.height = height;
            this.width = width;
            grid = new List<List<Cell>>();
            for (int i = 0; i < height; i++)
            {
                grid.Add(new List<Cell>());
                for (int j = 0; j < height; j++)
                {
                    grid[i].Add(new Cell(false));
                }
            }
        }
        public static Field GenerateRandomField(int height, int width)
        {
            Random random = new Random(146);
            Field result = new Field(height, width);
            if(height <= 0 || width <= 0)
                throw new ArgumentOutOfRangeException();
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    result.grid[i][j].Alive = random.Next(2) == 1;
            return result;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var line in grid)
            {
                foreach (var cell in line)
                    sb.Append(cell.ToString());
                sb.Append("\n");
            }
            return sb.ToString();
        }
        private List<Point> getAdjacent(Point position)
        {
            List<Point> result = new List<Point>();
            int[] dHeight = new int[] { 1, 1, 1, -1, -1, -1, 0, 0 };
            int[] dWidth = new int[] {1, 0, -1, 1, 0, -1, 1, -1};
            Func<int, int, bool> valid = (y, x) =>
                {
                    return 0 <= y && y < height && 0 <= x && x < width;
                };
            for (int k = 0; k < 8; k++)
            {
                Point currentCellCoordinates =
                    new Point(position.X + dHeight[k], position.Y + dWidth[k]);
                if (valid(currentCellCoordinates.X, currentCellCoordinates.Y))
                {
                    result.Add(currentCellCoordinates);
                }
            }
            return result;
        }
        public Field nextStep()
        {
            Field result = new Field(height, width);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    List<Point> adjacent = getAdjacent(new Point(i, j));
                    int adjacentAliveCount = 0;
                    foreach (var adjCellCoordinates in adjacent)
                    {
                        if (grid[adjCellCoordinates.X][adjCellCoordinates.Y].Alive)
                            adjacentAliveCount++;
                    }

                    if (grid[i][j].Alive)
                    {
                        if (2 <= adjacentAliveCount && adjacentAliveCount <= 3)
                            result.grid[i][j].Alive = true;
                    }
                    else
                    {
                        if (3 == adjacentAliveCount)
                            result.grid[i][j].Alive = true;
                    }
                }
            }
            return result;
        }
    }
}
