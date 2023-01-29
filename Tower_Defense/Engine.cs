using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Tower_Defense
{
    public class Engine
    {
        public static Tile[,] tiles;
        public static Form1 form;

        public static List<Tower> towers = new List<Tower>();
        public static List<Enemy> enemies = new List<Enemy>() , currentWave = new List<Enemy>();
        public static List <Projectile> projectiles = new List<Projectile>();

        public static int tilex, tiley, nrx, nry ;
        public static double castleHealth = 100 , time = 0;

        public static bool isBlur;

        public static Bitmap bitmap;
        public static Graphics graphics;

        public static Image enemyImage = Image.FromFile("../../Images/orc.png");

        public static void Initialize(Form1 f)
        {
            form = f;
            nrx=12;
            nry=8;
            tilex=form.pictureBox1.Width/12;
            tiley=form.pictureBox1.Height/8;
            bitmap = new Bitmap(form.pictureBox1.Width, form.pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            CreateTiles();

            currentWave.Add(new Enemy(enemyImage, 15, 20, 50, tilex, tiley , 0));
            currentWave.Add(new Enemy(enemyImage, 15, 20, 50, tilex, tiley, 5));
            currentWave.Add(new Enemy(enemyImage, 10, 20, 50, tilex, tiley, 15));
            currentWave.Add(new Enemy(enemyImage, 10, 20, 50, tilex, tiley, 25));
            currentWave.Add(new Enemy(enemyImage, 15, 20, 50, tilex, tiley, 35));

        }
        public static void Tick()
        {
            time++; 
            if (currentWave.Any() && currentWave[0].spawntime <= time)
            {
                enemies.Add(currentWave[0]);
                currentWave.RemoveAt(0);
            }
            MoveEnemies();
            ShootEverything();
            Lose();
        }
        private static void ShootEverything()
        {
            foreach (Tower tower in towers)
            {
                Enemy target = tower.DetectEnemy();
                if(target != null)
                    tower.Shoot(target);
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Move();
            }
        }

        public static void MoveEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                if (enemy.Move())
                {
                    castleHealth -= enemy.damage;
                    enemies.Remove(enemies[i]);
                    i--;
                }
            }
        }
        public static void Lose()
        {

            if (castleHealth <= 0)
            {
                form.timer1.Stop();
                form.pictureBox5.ImageLocation = "../../Images/elmo-fire.gif";
            }
        }
        public static void CreateTiles()
        {
            tiles = new Tile[nrx,nry];

            for (int i = 0; i < nrx; i++)
            {
                for (int j = 0; j < nry; j++)
                {
                    tiles[i, j] = new Tile();
                }
            }
            Placeble();
        }

        public static void AddNewTower(Point location)
        {
            if (tiles[location.X/tilex, location.Y/tiley].isPlaceable)
            {
                isBlur = false;
                Image img;
                float range; 
                if (form.ID == 1)
                {
                    img = form.pictureBox2.Image;
                    range = 150;
                }
                else if(form.ID == 2)
                {
                    img = form.pictureBox3.Image;
                    range = 200;
                }
                else
                {
                    img = form.pictureBox4.Image;
                    range = 100;
                }
                Tower tower = new Tower(range, 0, 0, tilex, tiley, img, new Point(location.X /tilex * tilex, location.Y / tiley * tiley));
                towers.Add(tower);
                tiles[location.X/tilex, location.Y/tiley].isPlaceable = false;
            }
            DrawEverything();
        }

        public static bool Placeble()
        {
            for (int i = 0; i < nrx; i++)
            {
                for (int j = 0; j < nry; j++)
                {
                    if (i==1 && j<7)
                        tiles[i, j].isPlaceable = false;
                    if ((j==6 && i<5 && i>0) || (j==6 && i<11 && i>6))
                        tiles[i, j].isPlaceable = false;
                    if ((i==4 && j>0 && j< 7) || (i==10 && j>0 && j< 7))
                        tiles[i, j].isPlaceable = false;
                    if (j==1 && i>3 && i<11)
                        tiles[i, j].isPlaceable = false;
                    if (i==7 && j>3 && j<7)
                        tiles[i, j].isPlaceable = false;
                }
            }
            tiles[0, 5].isPlaceable = false;
            tiles[2, 7].isPlaceable = false;
            tiles[3, 2].isPlaceable = false;
            tiles[3, 3].isPlaceable = false;
            tiles[3, 3].isPlaceable = false;
            tiles[6, 0].isPlaceable = false;
            tiles[7, 0].isPlaceable = false;
            tiles[7, 7].isPlaceable = false;
            tiles[8, 7].isPlaceable = false;
            tiles[11, 3].isPlaceable = false;
            return true;
        }
        public static void DrawEverything()
        {
            graphics.DrawImage(form.background, 0, 0, form.pictureBox1.Width, form.pictureBox1.Height);
            if (isBlur)
            {
                Blurbackground();
            }
            foreach (Tower tower in towers)
            {
                tower.DrawRange();
                tower.Draw();
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw();
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw();
            }
            form.pictureBox1.Image = bitmap; 
        }
        
        public static void Blurbackground()
        {
            for (int i = 0; i < nrx; i++)
            {
                for (int j = 0; j < nry; j++)
                {
                    if(!tiles[i,j].isPlaceable)
                    {
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.Red)),i * tilex,j * tiley, tilex,tiley);
                    }
                    else
                    {
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.Green)), i * tilex, j * tiley, tilex, tiley);
                    }
                }
            }
            form.pictureBox1.Image = bitmap;
        }

        public static float Distance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
    }
}
