using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public abstract class Bonus
    {
        readonly Point position;
        public Point Position { get { return position; } }

        public Bonus(Point position)
        {
            this.position = position;
        }

        public void GetBonusType()
        {
            throw new System.NotImplementedException();
        }
    }
}