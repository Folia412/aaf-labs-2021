using System;
using System.Collections.Generic;
using System.Text;

namespace P1
{
    public class Rectangle
    {
        public int x_left, x_right, y_bottom, y_top;

        public Rectangle(int x1, int x2, int y1, int y2)
        {
            x_left = x1;
            x_right = x2;
            y_bottom = y1;
            y_top = y2;
        }
    }
}
