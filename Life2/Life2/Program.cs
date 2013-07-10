using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Life2
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = Game.RandomGame(50, 50);
            while (true)
            {
                Console.Clear();
                Console.WriteLine(game.ToString());
                game.Step();
                System.Threading.Thread.Sleep(100);
            }
        }
    }

    public class Game
    {
        public Cell[,] Field;

        public Game(int x, int y)
        {
            Field = new Cell[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Field[i,j] = new Cell();
                }
            }
        }
        public static Game RandomGame(int x, int y)
        {
            var game = new Game(x, y);
            var rand = new Random();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    game.Field[i, j].Alive = rand.Next(2)==1?true:false;
                }
            }
            return game;
        }

        public int countAliveAdjacent(int x, int y)
        {
            int[] dHeight = new int[] {1, 1, 1, 0, 0, -1, -1, -1};
            int[] dWidth = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
            var count = 0;
            for (int i = 0; i < dHeight.Length; i++)
            {
                var newX = x + dHeight[i];
                var newY = y + dWidth[i];

                if (!(newX < 0 || newY < 0 || newX >= Field.GetLength(0) || newY >= Field.GetLength(1)))
                    if(Field[newX,newY].Alive)
                        count ++;
            }
            return count;
        }

        public void Step()
        {
            var x = Field.GetLength(0);
            var y = Field.GetLength(1);
            Cell[,] newField = new Cell[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    newField[i,j] = new Cell();
                }
            }
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    var count = this.countAliveAdjacent(i, j);
                    newField[i, j].Alive = count == 2
                                               ? Field[i, j].Alive
                                               : count == 3 ? true : false;
                }
            }
            this.Field = newField;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            var x = Field.GetLength(0);
            var y = Field.GetLength(1);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    sb.Append(this.Field[i, j].ToString());
                }
                sb.Append("\n");
            }
            return sb.ToString();

        }
    }

    public class Cell
    {
        public Cell()
        {
            this.Alive = false;
        }
        public bool Alive { get; set; }
        public override string ToString()
        {
            return this.Alive ? "#" : " ";
        }
    }


    [TestFixture]
    public class Cell_Test
    {
        [Test]
        public void Test()
        {
            Cell c = new Cell();
            Assert.AreEqual(c.Alive, false);

        }
    }

    public class Game_Test
    {
        [TestCase(1,2)]
        public void TestField(int x, int y)
        {
            Game g = new Game(x, y);
            Assert.AreEqual(g.Field.GetLength(0), x);
        }

        [Test]
        public void TestAdjacent()
        {
            Game g = new Game(5, 5);
            g.Field[0, 1].Alive = true;
            Assert.AreEqual(1, g.countAliveAdjacent(0, 0));
        }

        [Test]
        public void TestDie()
        {
            Game g = new Game(5, 5);
            g.Field[0, 1].Alive = true;
            g.Step();
            Assert.AreEqual(false, g.Field[0,1].Alive);
        }

    }
}
