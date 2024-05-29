using System;
using System.Collections.Generic;
using System.Drawing;

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
            currentPosition = new PathPoint(new PointF(Engine.tilex + (float)(Engine.tilex-sizex)/2, (float)(Engine.tiley - sizey) / 2), "south");

            path.Add(new PathPoint(new PointF(Engine.tilex + (float)(Engine.tilex-sizex)/2, 6 * Engine.tiley + (float)(Engine.tiley - sizey) / 2), "east"));
            path.Add(new PathPoint(new PointF(4 * Engine.tilex + (float)(Engine.tilex-sizex)/2, 6 * Engine.tiley + (float)(Engine.tiley - sizey) / 2), "north"));
            path.Add(new PathPoint(new PointF(4 * Engine.tilex + (float)(Engine.tilex-sizex)/2, Engine.tiley + (float)(Engine.tiley - sizey) / 2), "east"));
            path.Add(new PathPoint(new PointF(10 * Engine.tilex + (float)(Engine.tilex-sizex)/2, Engine.tiley + (float)(Engine.tiley - sizey) / 2), "south"));
            path.Add(new PathPoint(new PointF(10 * Engine.tilex + (float)(Engine.tilex-sizex)/2, 6 * Engine.tiley + (float)(Engine.tiley - sizey) / 2), "west"));
            path.Add(new PathPoint(new PointF(7 * Engine.tilex + (float)(Engine.tilex-sizex)/2, 6 * Engine.tiley + (float)(Engine.tiley - sizey) / 2), "north"));
            path.Add(new PathPoint(new PointF(7 * Engine.tilex + (float)(Engine.tilex-sizex)/2, 4 * Engine.tiley + (float)(Engine.tiley - sizey) / 2), "finish"));
        }
        public bool Move()
        {
            if (!Engine.isPaused)
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
                    case "south": currentPosition.point = new PointF(currentPosition.point.X, currentPosition.point.Y + (float)speed); break;
                    case "east": currentPosition.point = new PointF(currentPosition.point.X + (float)speed, currentPosition.point.Y); break;
                    case "north": currentPosition.point = new PointF(currentPosition.point.X, currentPosition.point.Y - (float)speed); break;
                    case "west": currentPosition.point = new PointF(currentPosition.point.X - (float)speed, currentPosition.point.Y); break;
                    case "finish":
                    default:
                        return true;
                }
            }
            return false;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            Console.WriteLine("Inamic a primit " + damage + " damage. Health: " + health);
        }
        public void Draw()
        {
            Engine.graphics.DrawImage(image, currentPosition.point.X, currentPosition.point.Y, (float)sizex, (float)sizey);
        }
    }

    
}
