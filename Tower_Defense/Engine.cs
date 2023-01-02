using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tower_Defense
{
    public class Engine
    {
        public static Tile[,] tiles;
        public static Form1 form;
        public static List<Tower> towers = new List<Tower>();

        public static int tilex, tiley, nrx, nry;

        public static Bitmap bitmap;
        public static Graphics graphics;

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
                Tower tower = new Tower(0, 0, 0, tilex, tiley, form.pictureBox2.Image, new Point(location.X /tilex * tilex, location.Y / tiley * tiley));
                towers.Add(tower);
                
            }
            DrawAllTowers();
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
            tiles[0, 5].isPlaceable=false;
            tiles[2, 7].isPlaceable=false;
            tiles[3, 2].isPlaceable=false;
            tiles[3, 3].isPlaceable=false;
            tiles[3, 3].isPlaceable=false;
            tiles[6, 0].isPlaceable=false;
            tiles[7, 0].isPlaceable=false;
            tiles[7, 7].isPlaceable=false;
            tiles[8, 7].isPlaceable=false;
            tiles[11, 3].isPlaceable=false;
            return true;
        }
        public static void DrawAllTowers()
        {
            graphics.DrawImage(form.background, 0, 0, form.pictureBox1.Width, form.pictureBox1.Height);
              foreach (Tower tower in towers)
              {
                  tower.Draw();
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
            form.pictureBox1.Image= bitmap;
        }
    }
}
