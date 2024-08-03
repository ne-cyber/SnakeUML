using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public class FieldRandomizer
    {
        Random random = new Random();
        public Point RandPoint(Field field)
        {
            while (true)
            {

                Point newPoint = new Point()
                {
                    X = random.Next(field.Width),
                    Y = random.Next(field.Height)
                };

                bool positionFree = true;



                if (field.Bonuses != null)
                {
                    for (int i = 0; i < field.Bonuses.Length; i++)
                    {
                        if (field.Bonuses[i] != null && field.Bonuses[i].Position == newPoint)
                            positionFree = false;
                    }
                }

                if (field.SpecialItems != null)
                {
                    for (int i = 0; i < field.SpecialItems.Length; i++)
                    {
                        if (field.SpecialItems[i] == null)
                            continue;

                        if (!(field.SpecialItems[i] is DualHole))
                            throw new Exception("Known SpecialItem only DualHole.");

                        DualHole dualHole = (DualHole)field.SpecialItems[i];

                        if (dualHole.Position1 == newPoint || dualHole.Position2 == newPoint)
                            positionFree = false;
                    }
                }

                if (field.Snake != null)
                {
                    for (int i = 0; i < field.Snake.Body.Length; i++)
                    {
                        if (field.Snake.Body[i] != null && field.Snake.Body[i] == newPoint)
                            positionFree = false;
                    }
                }

                if (positionFree)
                    return newPoint;
            }
        }

    }
}
