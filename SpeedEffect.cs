using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public class GameSpeedEffect : GameEffect
    {
        public GameSpeedEffect(DateTime started, int duration, double speedRatio)
            : base(started, duration) 
        {
            this.speedRatio = speedRatio;
        }

        readonly double speedRatio;

        public double SpeedRatio
        {
            get { return speedRatio; }
        }
        
    }
}