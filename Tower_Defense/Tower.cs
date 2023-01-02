using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_Defense
{
    public class Tower
    {
        public double range, attack ,sizex , sizey;
        public int cost;
        public Image image;
        public Point position;
        public Tower(double range,double attack ,int cost,double sizex ,double sizey ,Image image, Point point)
        {
            this.range = range;
            this.attack = attack;
            this.cost = cost;
            this.sizex = sizex;
            this.sizey = sizey;
            this.image = image;
            this.position = point;
        }
        public void Draw()
        {
            Engine.graphics.DrawImage(image, position.X,position.Y, (int)sizex, (int)sizey);
        }
    }
}
