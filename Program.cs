using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeUML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            // Level 2. Game speed normal
            game.NextTurnInterval = 250;
            game.Run();

            Console.ReadKey();
        }
    }
}
