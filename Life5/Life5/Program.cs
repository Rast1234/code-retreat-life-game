using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Life5
{
    class Program
    {
        static void Main()
        {
            var game = new Game(new HashSet<Size>
                {
                    new Size(0, 0),
                    new Size(15, 15), new Size(16, 14), new Size(16, 15), 
                    new Size(16, 16), new Size(17, 16), 
                });
            Run(game);
        }
        static void Run(Game game)
        {
            SlowConsolePrint(game.ToString());
            Run(new Game(game.NextStep()));
        }
        static void SlowConsolePrint(String output)
        {
            Thread.Sleep(200);
            ConsolePrinting(output);
        }
        static void ConsolePrinting(String output)
        {
            Console.Clear();
            Console.WriteLine(output);
        }
    }
}
