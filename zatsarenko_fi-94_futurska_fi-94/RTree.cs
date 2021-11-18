using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    public class RTree
    {
        public Node root;
        Node node;

        public RTree()
        {
            root = null;
        }

        public void Insert(int x, int y)
        {
            if (root == null)
                FirstInsert(x, y);
            else
                LastInsert(root, x, y);
        }

        public int Print_Tree()
        {
            string s = "";
            if (root != null)
            {
                Print(root, s);
                return 1;
            }
            return 0;
        }

        public bool Contains(int x, int y)
        {
            return Contain(root, x, y);
        }

        public int Search()
        {
            int z = Search(root, 0);
            Console.WriteLine();
            return z;
        }

        public int Search(int xl, int yb, int xr, int yt)
        {
            int z = Search(root, xl, yb, xr, yt, 0);
            Console.WriteLine();
            return z;
        }

        public int Search(int x)
        {
            int z = Search(root, x, 0);
            Console.WriteLine();
            return z;
        }

        public int Search(int x, int y)
        { 
            if (root == null)
                return 0;
            string[] output = { "-1", "" };
            output = Search(root, x, y, output);
            string s = output[1];
            s = s.Remove(s.Length - 2);
            Console.WriteLine(s);
            return 1;
        }

        private string[] Search(Node node, int x, int y, string[] output)
        {
            if (node == null) return output;
            if (node.rec != null)
            {
                output = Search(node.left, x, y, output);
                output = Search(node.right, x, y, output);
            }
            else
            {
                var p = node.point;
                int d = PointsDistance(x, y, p.x, p.y);
                if (output[0] == "-1")
                {
                    output[0] = d.ToString();
                    output[1] = "(" + p.x.ToString() + ", " + p.y.ToString() + "), ";
                }
                else
                {
                    if (Convert.ToInt32(output[0]) > d)
                    {
                        output[0] = d.ToString();
                        output[1] = "(" + p.x.ToString() + ", " + p.y.ToString() + "), ";
                    }
                    else if (Convert.ToInt32(output[0]) == d)
                        output[1] = output[1] + "(" + p.x.ToString() + ", " + p.y.ToString() + "), ";
                }
            }
            return output;
        }

        private int Search(Node node, int z, int k)
        {
            if (node == null) return k;
            if (node.rec != null)
            {
                if (node.rec.x_left <= z)
                {
                    k = Search(node.left, z, k);
                    k = Search(node.right, z, k);
                }
            }
            else
            {
                var p = node.point;
                if (p.x < z)
                {
                    if (k == 0)
                        Console.Write("({0}, {1})", p.x, p.y);
                    else
                        Console.Write(", ({0}, {1})", p.x, p.y);
                    return 1;
                }
            }
            return k;
        }

        private int Search(Node node, int xl, int yb, int xr, int yt, int k)
        {
            if (node == null) return k;
            if (node.rec != null)
            {
                k = Search(node.left, xl, yb, xr, yt, k);
                k = Search(node.right, xl, yb, xr, yt, k);
            }
            else
            {
                var p = node.point;
                if (p.x >= xl & p.x <= xr & p.y >= yb & p.y <= yt)
                {
                    if (k == 0)
                        Console.Write("({0}, {1})", p.x, p.y);
                    else
                        Console.Write(", ({0}, {1})", p.x, p.y);
                    return 1;
                }
            }
            return k;
        }

        private int Search(Node node, int k)
        {
            if (node == null) return k;
            if (node.rec != null)
            {
                k = Search(node.left, k);
                k = Search(node.right, k);
            }
            else
            {
                var p = node.point;
                if (k == 0)
                    Console.Write("({0}, {1})", p.x, p.y);
                else
                    Console.Write(", ({0}, {1})", p.x, p.y);
                return 1;
            }
            return k;
        }

        private bool Contain(Node node, int x, int y)
        {
            if (node == null) return false;
            var r = node.rec;
            if (r == null)
            {
                var p = node.point;
                if (p.x == x & p.y == y)
                    return true;
            }
            else if (x >= r.x_left & x <= r.x_right & y >= r.y_bottom & y <= r.y_top)
            {
                if (Contain(node.left, x, y) == true)
                    return true;
                if (Contain(node.right, x, y) == true)
                    return true;
            }
            return false;
        }

        private int Print(Node node, string s)
        {
            if (node == null)
                return 0;
            var r = node.rec;
            if (r != null)
            {
                Console.WriteLine(s + "[ ({0}, {1}), ({2}, {3}) ]", r.x_left, r.y_bottom, r.x_right, r.y_top);
                Print(node.left, "     " + s);
                Print(node.right, "     " + s);
            }
            var p = node.point;
            if (p != null)
                Console.WriteLine(s + "({0}, {1})", p.x, p.y);
            return 0;
        }

        private void FirstInsert(int x, int y)
        {
            root = new Node(null, x, x, y, y);
            node = root;
            node.left = new Node(node, x, x, y, y);
            node = node.left;
            node.left = new Node(node, x, y);
        }

        private void LastInsert(Node node, int x, int y)
        {
            var r = node.rec;
            Rectangle rl, rr;
            if (x < r.x_left || x > r.x_right || y < r.y_bottom || y > r.y_top)
                node = IncreaseBoundaries(node, x, y);
            rl = node.left.rec;
            if (node.right != null)
                rr = node.right.rec;
            else rr = null;
            if (rl == null & rr == null)
                InsertPoint(node, x, y);
            else if (rr == null)
            {
                if (x >= rl.x_left & x <= rl.x_right & y >= rl.y_bottom & y <= rl.y_top)
                    LastInsert(node.left, x, y);
                else
                {
                    node.right = new Node(node, x, x, y, y);
                    node = node.right;
                    node.left = new Node(node, x, y);
                }
            }
            else
            {
                if (x >= rl.x_left & x <= rl.x_right & y >= rl.y_bottom & y <= rl.y_top)
                    LastInsert(node.left, x, y);
                else if (x >= rr.x_left & x <= rr.x_right & y >= rr.y_bottom & y <= rr.y_top)
                    LastInsert(node.right, x, y);
                else
                {
                    var sl = Distance(node.left, x, y);
                    var sr = Distance(node.right, x, y);
                    if (sl <= sr)
                        LastInsert(node.left, x, y);
                    else LastInsert(node.right, x, y);
                }
            }
        }

        private void InsertPoint(Node node, int x, int y)
        {
            Point pl, pr;
            pl = node.left.point;
            if (node.right != null)
                pr = node.right.point;
            else pr = null;
            if (pr == null)
                node.right = new Node(node, x, y);
            else
                SplitRectange(node, x, y);
        }

        private int Distance(Node node, int px, int py)
        {
            var r = node.rec;
            var rxl = r.x_left;
            var rxr = r.x_right;
            var ryb = r.y_bottom;
            var ryt = r.y_top;
            var s0 = (rxr - rxl) * (ryt - ryb);
            if (rxl > px)
                rxl = px;
            else if (rxr < px)
                rxr = px;
            if (ryb > py)
                ryb = py;
            else if (ryt < py)
                ryt = py;
            var s1 = (rxr - rxl) * (ryt - ryb);
            return s1 - s0;
        }

        private Node IncreaseBoundaries(Node node, int x, int y)
        {
            var r = node.rec;
            if (r.x_left > x)
                r.x_left = x;
            else if (r.x_right < x)
                r.x_right = x;
            if (r.y_bottom > y)
                r.y_bottom = y;
            else if (r.y_top < y)
                r.y_top = y;
            return node;
        }

        private void SplitRectange(Node node, int x, int y)
        {
            int x1 = node.left.point.x;
            int y1 = node.left.point.y;
            int x2 = node.right.point.x;
            int y2 = node.right.point.y;
            node.left.point = null;
            node.right.point = null;
            int d0 = PointsDistance(x, y, x1, y1);
            int d1 = PointsDistance(x1, y1, x2, y2);
            int d2 = PointsDistance(x2, y2, x, y);
            int min = Math.Min(d0, Math.Min(d1, d2));
            if (d0 == min)
            {
                CreateRectange(node, x, y, x1, y1);
                CreateRectange(node, x2, y2);
            }
            else if (d1 == min)
            {
                CreateRectange(node, x1, y1, x2, y2);
                CreateRectange(node, x, y);
            }
            else
            {
                CreateRectange(node, x2, y2, x, y);
                CreateRectange(node, x1, y1);
            }
        }

        private int PointsDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) * Math.Abs(y1 - y2);
        }

        private void CreateRectange(Node node, int x1, int y1, int x2, int y2)
        {
            int x_min = Math.Min(x1, x2);
            int x_max = Math.Max(x1, x2);
            int y_min = Math.Min(y1, y2);
            int y_max = Math.Max(y1, y2);
            node.left = new Node(node, x_min, x_max, y_min, y_max);
            var temp = node.left;
            temp.left = new Node(temp, x1, y1);
            temp.right = new Node(temp, x2, y2);
        }

        private void CreateRectange(Node node, int x, int y)
        {
            node.right = new Node(node, x, x, y, y);
            var temp = node.right;
            temp.left = new Node(temp, x, y);
        }
    }
}
