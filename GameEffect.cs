using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeUML
{
    public abstract class GameEffect
    {
        protected readonly DateTime started;
        /// <summary>
        /// in milliseconds
        /// </summary>
        protected readonly int duration;
        public DateTime Started
        {
            get { return started; }
        }

        public int Duration
        {
            get { return duration; }
        }

        public bool TimeIsOver()
        {
            TimeSpan passed = DateTime.Now - started;

            if (passed.TotalMilliseconds > Duration)
                return true;
            else
                return false;
        }

        public GameEffect(DateTime started, int duration)
        {
            this.started = started;
            this.duration = duration;
        }
    }
}