using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public class BonusGameEffectVisual : Bonus
    {
        readonly int duration;
        public int Duration { get { return duration; } }

        public BonusGameEffectVisual(Point position, int duration)
            : base(position)
        {
            this.duration = duration;
        }
    }
}