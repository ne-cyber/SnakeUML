using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public class BonusFabric
    {
        public Apple GenerateApple(Field field, FieldRandomizer fieldRandomizer)
        {
            Point position = fieldRandomizer.RandPoint(field);

            return new Apple(position);
        }

        public Speed GenerateSpeed(Field field, FieldRandomizer fieldRandomizer)
        {
            Point position = fieldRandomizer.RandPoint(field);

            return new Speed(position);
        }

        public BonusGameEffectVisual GenerateBonusGameEffectVisual(Field field, FieldRandomizer fieldRandomizer)
        {
            Point position = fieldRandomizer.RandPoint(field);

            return new BonusGameEffectVisual(position, 5);
        }
    }
}