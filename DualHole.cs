using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public class DualHole : SpecialItem
    {
        readonly Point position1;
        readonly Point position2;

        public Point Position1
        {
            get { return position1; }
        }
        public Point Position2
        {
            get { return position2; }
        }


        public DualHole(Point position1, Point position2)
        {
            this.position1 = position1;
            this.position2 = position2;
        }
    }

}