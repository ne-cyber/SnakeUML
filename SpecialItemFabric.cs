using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public class SpecialItemFabric
    {

        public DualHole GenerateDualHole(Field field, FieldRandomizer fieldRandomizer)
        {
            Point position1;
            Point position2;
            do
            {
                position1 = fieldRandomizer.RandPoint(field);
                position2 = fieldRandomizer.RandPoint(field);
            }
            while (position1 == position2);

            return new DualHole(position1, position2);
        }
    }
}