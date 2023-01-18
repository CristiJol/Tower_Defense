﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_Defense
{
    public class Projectile
    {
        public double speed , sizex , sizey;
        public int damage;
        public Image image;
        public PointF position;
        public Enemy target;

        public Projectile(Image image,double speed , int damage ,double sizex,double sizey ,PointF position , Enemy target)
        {
            this.speed = speed;
            this.damage = damage;
            this.sizex = sizex;
            this.sizey = sizey;
            this.position = position;
            this.image = image;
            this.target = target;
        }
        public void Move()
        {
            //float percent = (float)speed / Engine.Distance(position, target.currentPosition.point);
            //float x = (1 - percent) * position.X + percent * target.currentPosition.point.X;
            //float y = (1 - percent) * position.Y + percent * target.currentPosition.point.Y;
            //position = new PointF(y, x);

            float magnitude1, magnitude2;
            magnitude1 = (float)Math.Sqrt(position.X*position.X + position.Y*position.Y);
            magnitude2 = (float)Math.Sqrt(target.currentPosition.point.X * target.currentPosition.point.X + target.currentPosition.point.Y * target.currentPosition.point.Y);
            float dot_product = position.X * target.currentPosition.point.X + position.Y * target.currentPosition.point.Y;
            double teta = 0;
            teta = Math.Acos(dot_product/(magnitude1 * magnitude2));
            float x = position.X + (float)(speed * Math.Cos(teta));
            float y = position.Y + (float)(speed * Math.Sin(teta));
            position = new PointF(y, x);
            
            Engine.toDelete = new PointF(target.currentPosition.point.X, target.currentPosition.point.Y);
        }
        public void Draw()
        {
            Engine.graphics.DrawImage(image, position.X, position.Y, (int)sizex, (int)sizey);
        }
    }
}
