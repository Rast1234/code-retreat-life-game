using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Life3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var game = new Game(new int[,]
                {
                    {0, 0, 0, 0, 0},
                    {0, 0, 1, 0, 0},
                    {0, 0, 1, 0, 0},
                    {0, 0, 1, 0, 0},
                    {0, 0, 0, 0, 0}
                });
            while (true)
            {
                Console.Clear();
                game.run();
                System.Threading.Thread.Sleep(100);
            }
       
    }

    }
    public class Game
    {
        public int[,] Field;
        private int height;
        private int width;
        private static int[,] adj = {{-1, 1}, {0, 1}, {1, 1}, {-1, 0}, {1, 0}, {-1, -1}, {0, -1}, {1, -1}};

        public Game(int[,] arr)
        {
            this.Field = arr;
            this.width = Field.GetLength(0);
            this.height = Field.GetLength(1);
        }

        public int adjCount(int x, int y)
        {
            return Enumerable.Range(0, 8).Sum(i => Field[nextAdj(x, i, 0, width), nextAdj(y, i, 1, height)]);
        }

        public int nextAdj(int x, int i, int j, int size)
        {
            return x + adj[i, j] < 0 ? size - 1 : (x + adj[i, j])%size;
        }

        public void Update()
        {
            var newField = new int[height, width];
            generate(newField);
            Field = newField;
        }

        public void generate(int[,] acc)
        {
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    acc[i, j] = newState(i, j);
        }

        public int newState(int x, int y)
        {
            int aliveAdj = adjCount(x, y);
            return aliveAdj == 2 ? Field[x, y] : (aliveAdj == 3 ? 1 : 0);
        }
        public void run()
        {
            Update();
            Console.WriteLine(this.ToString());
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                    sb.Append(Field[i, j] == 1 ? "#" : "_");
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }

    [TestFixture]
    public class Program_Test
    {
        [TestCase(2, 4, 0, 3, 0)]
        [TestCase(0, 3, 0, 3, 2)]
        [TestCase(0, 6, 1, 4, 3)]
        public void NextAdjTest(int x, int i, int j, int s, int expected)
        {
            var game = new Game(new int[,]
                {
                    {0, 0, 0},
                    {0, 0, 0},
                    {0, 0, 0},
                    {0, 0, 0}
                });
            Assert.AreEqual(expected, game.nextAdj(x, i, j, s));
        }

        [Test]
        public void newStateTest()
        {
            var game = new Game(new int[,]
                {
                    {0, 0, 0, 0, 0},
                    {0, 0, 1, 0, 0},
                    {0, 0, 1, 0, 0},
                    {0, 0, 1, 0, 0},
                    {0, 0, 0, 0, 0}
                });
            Assert.AreEqual(game.newState(2, 1), 1);
        }
    }
}
