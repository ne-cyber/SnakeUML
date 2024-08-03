using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace SnakeUML
{
    
    public class Snake
    {
        public Point[] Body
        {
            get;
            set;
        }

        public Point Head
        {
            get { return Body[0]; }
        }

        
        public Point Direction
        {
            get;
            set;
        }


        public void Initialize()
        {
            Body = new Point[4] { new Point(3, 0), new Point(2, 0), new Point(1, 0), new Point(0, 0) };
            Direction = new Point(1, 0);
        }

        public Snake()
        {
            Initialize();
        }
    }
}