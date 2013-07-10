using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Life4
{
    class Program
    {
        public class Game
        {
            public int[,] Field;
            public int[,] FieldCopy;
            public int xsize;
            public int ysize;
            public bool[,] used;

            public Game(int[,] field)
            {
                Field = field;

                xsize = field.GetLength(0)-1;
                ysize = field.GetLength(1) - 1;                                
            }
            public int countAlive(int x, int y)
            {
                return Field[x - 1, y - 1] + Field[x, y - 1] + Field[x + 1, y - 1] + Field[x + 1, y] +
                       Field[x + 1, y + 1] + Field[x, y + 1] + Field[x - 1, y + 1] + Field[x - 1, y];
            }

            public void NextStep()
            {
                used = new bool[xsize + 10, ysize + 10];
                Show(1, 1);

                used = new bool[xsize + 10, ysize + 10];
                FieldCopy = new int[xsize + 1, ysize + 1];
                bfs(1, 1);
                Field = FieldCopy;                
                Thread.Sleep(200);
            }

            public void run()
            {
                NextStep();
                run();
            }

            public void Show(int x, int y)
            {
                Enumerable.Range(0, 10).Select(i => { sdfsdf }).ToArray();
                if (used[x, y] == true || x >= xsize || y >= ysize) return;                
                used[x, y] = true;
                Console.SetCursorPosition(x, y);
                Console.Write( Field[x, y] == 1 ? '*' : ' ' );

                Show(x + 1, y);
                Show(x, y + 1);
                Show(x + 1, y + 1);
            }
            

            public void bfs(int x, int y)
            {
                if (used[x, y] == true || x >= xsize || y >= ysize) return;
                used[x, y] = true;
                int heig = countAlive(x, y);
                if (heig < 2 || heig > 3)
                {
                    FieldCopy[x, y] = 0;
                } else if (heig == 3)
                {
                    FieldCopy[x, y] = 1;
                }
                else
                {
                    FieldCopy[x, y] = Field[x, y];
                }

                bfs(x + 1, y);
                bfs(x + 1, y + 1);
                bfs(x, y + 1);
            }
        }
        static void Main(string[] args)
        {
            var game = new Game(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                });
            game.run();
        }
    }
}
