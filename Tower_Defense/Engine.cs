using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Tower_Defense
{
    public class Engine
    {
        public static bool isPaused
        {
            get { return form.isPaused; }
        }
        public static bool isGameWon = false;

        

        public static Tile[,] tiles;
        public static Form1 form;

        public static Tower selectedTower;
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
            if (!isPaused)
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

                if (isGameWon)
                {
                    // Afișează fereastra de felicitare și revino la ecranul principal după 5 secunde
                    isGameWon = false;
                    ShowVictoryScreen();
                }
            }
        }
        public static void ShootEverything()
        {
            foreach (Tower tower in towers)
            {
                Enemy target = tower.DetectEnemy();
                if (target != null)
                    tower.Shoot(target);
            }
            foreach (Projectile projectile in projectiles.ToList())
            {
                if (!isPaused) // Adaugă această verificare pentru a opri mișcarea proiectilelor în timpul pauzei
                {
                    projectile.Move();
                    CheckProjectileEnemyCollisions();
                }
            }
            // Elimină inamicul după ce viața sa a ajuns sub 0
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].health <= 0)
                {
                    enemies.Remove(enemies[i]);
                    i--;
                    form.CurrentMoney += 25;
                }
            }
            if (enemies.Count == 0 && currentWave.Count == 0)
            {
                isGameWon = true;
            }
        }
        public static void MoveEnemies()
        {
            if (!isPaused)
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
            if (tiles[location.X / tilex, location.Y / tiley].isPlaceable)
            {
                isBlur = false;
                Image img=null;
                float range=0;
                int cost = 0;
                int attack=0; // Adaugă o variabilă pentru a stoca valoarea de atac (damage) în funcție de form.ID

                // Atribuie valoarea de atac corespunzătoare în funcție de form.ID
                if (form.ID == 1)
                {
                    img = form.pictureBox2.Image;
                    range = 150;
                    cost = 50;
                    attack = 20; // Schimbă valoarea în funcție de nevoile tale
                }
                else if (form.ID == 2)
                {
                    img = form.pictureBox3.Image;
                    range = 200;
                    cost = 150;
                    attack = 30; // Schimbă valoarea în funcție de nevoile tale
                }
                else if (form.ID == 3)
                {
                    img = form.pictureBox4.Image;
                    range = 100;
                    cost = 200;
                    attack = 15; // Schimbă valoarea în funcție de nevoile tale
                }
                if (img != null && cost <= form.CurrentMoney)
                {
                    Tower tower = new Tower(range, cost, tilex, tiley, img, new Point(location.X / tilex * tilex, location.Y / tiley * tiley), attack);
                    towers.Add(tower);
                    selectedTower = tower;
                    tiles[location.X / tilex, location.Y / tiley].isPlaceable = false;
                    form.ID = 0;
                    form.CurrentMoney -= cost;
                }
            }
            DrawEverything();
        }
        public static void SelectTower(Point location)
        {
            foreach(Tower tower in towers)
            {
                if (tower.position == new Point(location.X / tilex * tilex, location.Y / tiley * tiley))
                    selectedTower = tower;
            }
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
                if (tower == selectedTower)
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
        public static void CheckProjectileEnemyCollisions()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                Projectile projectile = projectiles[i];

                foreach (Enemy enemy in enemies)
                {
                    // Verifică dacă proiectilul atinge inamicul
                    if (Distance(projectile.position, enemy.currentPosition.point) < projectile.sizex) // Setează o distanță prag aici
                    {
                        enemy.TakeDamage(projectile.damage);
                        projectiles.RemoveAt(i);
                        i--;
                        break; // Ieșim din bucla foreach, deoarece proiectilul a lovit deja un inamic
                    }
                }
            }
        }


        public static float Distance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        public static void ShowVictoryScreen()
        {
            form.timer1.Stop(); // Oprește timerul pentru a opri jocul
            CongratulationsForm congratulationsForm = new CongratulationsForm(); // Creați o nouă fereastră de felicitare
            congratulationsForm.ShowDialog(); // Afișați fereastra de felicitare

            System.Threading.Thread.Sleep(500);

            form.Close(); // Închide fereastra principală (Form1)
            StartScreenForm startScreenForm = new StartScreenForm(); // Deschide fereastra de start
            startScreenForm.ShowDialog();
        }

    }
}
