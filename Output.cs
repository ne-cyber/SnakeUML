using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public static class Output
    {
        static int visualEffectTurn = 0;
        public static void Render(Field field, GameVisualEffect visualEffect)
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            if (visualEffect != null)
            {
                if (visualEffectTurn == 0)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (visualEffectTurn == 1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (visualEffectTurn == 2)
                    Console.ForegroundColor = ConsoleColor.Blue;

                visualEffectTurn = (visualEffectTurn < 2 ? visualEffectTurn + 1 : 0);
            }

            Console.Clear();
            // Виводимо границі поля
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string('-', field.Width + 2));

            for (int j = 0; j < field.Height; j++)
            {
                Console.SetCursorPosition(0, 1 + j);
                Console.Write('|');

                Console.SetCursorPosition(1 + field.Width, 1 + j);
                Console.Write('|');
            }
            Console.WriteLine();

            Console.WriteLine(new string('-', field.Width + 2));

            // Виводимо бонуси
            for (int i = 0; i < field.Bonuses.Length; i++)
            {
                Console.SetCursorPosition(field.Bonuses[i].Position.X + 1, field.Bonuses[i].Position.Y + 1);
                if (field.Bonuses[i] is Apple)
                    Console.Write('a');
                else if (field.Bonuses[i] is Speed)
                    Console.Write('s');
                else if (field.Bonuses[i] is BonusGameEffectVisual)
                    Console.Write('V');
            }

            // Виводимо подвійну диру
            for (int i = 0; i < field.SpecialItems.Length; i++)
            {
                if (!(field.SpecialItems[i] is DualHole))
                    throw new Exception("only DualHole SpecialItem is known");
                DualHole dualHole = (DualHole)field.SpecialItems[i];
                Console.SetCursorPosition(dualHole.Position1.X + 1, dualHole.Position1.Y + 1);
                Console.Write('o');
                Console.SetCursorPosition(dualHole.Position2.X + 1, dualHole.Position2.Y + 1);
                Console.Write('o');

            }


            //Виводимо змійку
            for (int i = 0; i < field.Snake.Body.Length; i++)
            {
                Console.SetCursorPosition(field.Snake.Body[i].X + 1, field.Snake.Body[i].Y + 1);
                Console.Write('s');
            }

        }
    }
}