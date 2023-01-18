using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_Defense
{
    public class Enemy
    {
        public double speed, damage, sizex ,sizey;
        public int health , spawntime;
        public Image image;
        public List<PathPoint> path = new List<PathPoint>();
        public PathPoint currentPosition; 

        public Enemy (Image image , double speed ,double damage , int health ,double sizex,double sizey , int spawntime)
        {
            this.image = image;
            this.speed = speed;
            this.damage = damage;
            this.health = health;
            this.sizex = sizex;
            this.sizey = sizey;
            this.spawntime = spawntime;
            currentPosition = new PathPoint(new PointF((float)(Engine.tiley-sizey)/2, Engine.tilex + (float)(Engine.tilex-sizex)/2), "south");

            path.Add(new PathPoint(new PointF(6 * Engine.tiley + (float)(Engine.tiley-sizey)/2 , Engine.tilex + (float)(Engine.tilex-sizex)/2), "east"));
            path.Add(new PathPoint(new PointF(6 * Engine.tiley + (float)(Engine.tiley-sizey)/2, 4 * Engine.tilex + (float)(Engine.tilex-sizex)/2), "north"));
            path.Add(new PathPoint(new PointF(Engine.tiley + (float)(Engine.tiley-sizey)/2, 4 * Engine.tilex + (float)(Engine.tilex-sizex)/2), "east"));
            path.Add(new PathPoint(new PointF(Engine.tiley + (float)(Engine.tiley-sizey)/2, 10 * Engine.tilex + (float)(Engine.tilex-sizex)/2), "south"));
            path.Add(new PathPoint(new PointF(6 * Engine.tiley + (float)(Engine.tiley-sizey)/2, 10 * Engine.tilex + (float)(Engine.tilex-sizex)/2), "west"));
            path.Add(new PathPoint(new PointF(6 * Engine.tiley + (float)(Engine.tiley-sizey)/2, 7 * Engine.tilex + (float)(Engine.tilex-sizex)/2), "north"));
            path.Add(new PathPoint(new PointF(4 * Engine.tiley + (float)(Engine.tiley-sizey)/2, 7 * Engine.tilex + (float)(Engine.tilex-sizex)/2), "finish"));
        }
        public bool Move()
        {
            if (path[0].point.X - speed < currentPosition.point.X
                && path[0].point.X + speed > currentPosition.point.X
                && path[0].point.Y - speed < currentPosition.point.Y
                && path[0].point.Y + speed > currentPosition.point.Y)
            {
                currentPosition = path[0];
                path.RemoveAt(0);
            }
            switch (currentPosition.direction)
            {
                case "south": currentPosition.point = new PointF(currentPosition.point.X + (float)speed, currentPosition.point.Y); break;
                case "east": currentPosition.point = new PointF(currentPosition.point.X, currentPosition.point.Y + (float)speed); break;
                case "north": currentPosition.point = new PointF(currentPosition.point.X - (float)speed, currentPosition.point.Y); break;
                case "west": currentPosition.point = new PointF(currentPosition.point.X, currentPosition.point.Y - (float)speed); break;
                case "finish":
                default:
                    return true;
            }
            return false;
        }
        public void Draw()
        {
            Engine.graphics.DrawImage(image, currentPosition.point.Y, currentPosition.point.X, (float)sizex, (float)sizey);
        }
    }

    
}
