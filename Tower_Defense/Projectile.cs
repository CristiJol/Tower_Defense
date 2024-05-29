using System.Drawing;

namespace Tower_Defense
{
    public class Projectile
    {
        public double speed, sizex, sizey;
        public int damage;
        public Image image;
        public PointF position;
        public Enemy target;

        // Adaugă un constructor care să primească toți cei 8 parametri
        public Projectile(Image image, double speed, int damage, double sizex, double sizey, PointF position, Enemy target, double towerDamage)
        {
            this.image = image;
            this.speed = speed;
            this.damage = damage;
            this.sizex = sizex;
            this.sizey = sizey;
            this.position = position;
            this.target = target;
        }
        public void Move()
        {
            if (!Engine.isPaused) // Adaugă această verificare pentru a opri mișcarea proiectilului în timpul pauzei
            {
                float percent = (float)speed / Engine.Distance(position, target.currentPosition.point);
                float x = position.X + percent * (target.currentPosition.point.X - position.X);
                float y = position.Y + percent * (target.currentPosition.point.Y - position.Y);
                position = new PointF(x, y);

                if (Engine.Distance(position, target.currentPosition.point) < 5) // setează o distanță prag aici
                {
                    ApplyDamage();
                    // Elimină proiectilul sau efectuează alte acțiuni specifice
                }
            }
        }
        public void ApplyDamage()
        {
            target.TakeDamage((int)towerDamage); // Aplică damage-ul specific turnului
        }
        public void Draw()
        {
            Engine.graphics.DrawImage(image, position.X, position.Y, (int)sizex, (int)sizey);
        }

        public double towerDamage; // Adaugă atributul damage specific turnului



    }
}
