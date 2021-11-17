using System;
using System.Collections.Generic;
using System.Text;

namespace P1
{
    public class Node
    {
        public Node left, right, p;
        public Rectangle rec;
        public Point point;

        public Node(Node p, int x0, int y0, int x1, int y1)
        {
            this.p = p;
            left = null;
            right = null;
            rec = new Rectangle(x0, y0, x1, y1);
            point = null;
        }

        public Node(Node p, int x, int y)
        {
            this.p = p;
            left = null;
            right = null;
            rec = null;
            point = new Point(x, y);
        }
    }
}
