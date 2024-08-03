using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public static class Input
    {

        public static bool KeyAvaible()
        {
            return Console.KeyAvailable;
        }

        public static System.ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }
    }
}