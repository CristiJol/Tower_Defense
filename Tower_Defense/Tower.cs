using System.Drawing;

namespace Tower_Defense
{
    public class Tower
    {
        public double range, sizex, sizey;
        public int cost, attack, damage; // Adaugă atributul 'damage'
        public Image image;
        public Point position;

        // Adaugă o supraincarcare a constructorului cu parametrii opționali
        public Tower(double range, int cost, double sizex, double sizey, Image image, Point point, int attack = 20)
        {
            this.range = range;
            this.attack = attack;
            this.damage = attack; // Setează 'damage' cu valoarea 'attack'
            this.cost = cost;
            this.sizex = sizex;
            this.sizey = sizey;
            this.image = image;
            this.position = point;
        }

        public void Draw()
        {
            Engine.graphics.DrawImage(image, position.X, position.Y, (int)sizex, (int)sizey);
        }

        public void DrawRange()
        {
            var brush = new SolidBrush(Color.FromArgb(100, Color.Gray));
            Pen pen = new Pen(brush, 5);
            Engine.graphics.FillEllipse(brush, position.X + Engine.tilex / 2 - (float)range, position.Y + Engine.tiley / 2 - (float)range, (float)range * 2, (float)range * 2);
            Engine.graphics.DrawEllipse(pen, position.X + Engine.tilex / 2 - (float)range, position.Y + Engine.tiley / 2 - (float)range, (float)range * 2, (float)range * 2);
        }

        public Enemy DetectEnemy()
        {
            float minim = 10000;
            Enemy inamicumicu = null;
            float dist;
            foreach (Enemy enemy in Engine.enemies)
            {
                dist = Engine.Distance(enemy.currentPosition.point, position);
                if (dist < minim)
                {
                    minim = dist;
                    inamicumicu = enemy;
                }
            }
            if (minim < range)
            {
                return inamicumicu;
            }
            return null;
        }

        public void Shoot(Enemy enemy)
        {
            if (Engine.time % 10 == 0)
                Engine.projectiles.Add(new Projectile(image, 25, damage, Engine.tilex / 2, Engine.tiley / 2, new PointF(position.X + Engine.tilex / 2, position.Y + Engine.tiley / 2), enemy.currentPosition.point, (int)(range/25)));
            // ^ Utilizează 'damage' în loc de 'attack' în constructorul Projectile
        }
    }
}
