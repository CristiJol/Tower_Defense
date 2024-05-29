using System.Drawing;

namespace Tower_Defense
{
    public class Projectile
    {
        public double speed, sizex, sizey;
        public int damage;
        public Image image;
        public PointF position;
        public PointF target;
        public int lifespan;

        // Adaugă un constructor care să primească toți cei 8 parametri
        public Projectile(Image image, double speed, int damage, double sizex, double sizey, PointF position, PointF target, int lifespan)
        {
            this.image = image;
            this.speed = speed;
            this.damage = damage;
            this.sizex = sizex;
            this.sizey = sizey;
            this.position = position;
            this.target = target;
            this.lifespan = lifespan;
        }
        public void Move()
        {
            if (!Engine.isPaused) // Adaugă această verificare pentru a opri mișcarea proiectilului în timpul pauzei
            {
                float percent = (float)speed / Engine.Distance(position, target);
                float x = position.X + percent * (target.X - position.X);
                float y = position.Y + percent * (target.Y - position.Y);
                position = new PointF(x, y);
                lifespan--;
                if (lifespan <= 0)
                {
                    Engine.projectiles.Remove(this);
                }
            }
        }
        
        public void Draw()
        {
            Engine.graphics.DrawImage(image, position.X, position.Y, (int)sizex, (int)sizey);
        }


    }
}
