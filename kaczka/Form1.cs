using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kaczka
{

    public partial class Form1 : Form
    {

        Image kaczka,car1,car2,car3,car4;
        
        Bitmap BM_kaczka,BM_car1, BM_car2, BM_car3, BM_car4;
    
        bool goLeft, goRight, goUp, goDown;
        int speed = 10;
        int positionX=350;
        int positionY = 350;
        int height = 50;
        int width = 50;

        int x1,y1;
            Point main = new Point(350,350);
            //punkt startowy
            Point lewo1 = new Point(0, 233);
            Point prawo1 = new Point(800, 178);
            Point lewo2= new Point(-400, 233);
            Point prawo2 = new Point(1200, 178);
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        public Form1()
        {
            InitializeComponent();
            //tło
            this.BackgroundImage = Image.FromFile("road.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            //tworzenie gracza
            kaczka = Image.FromFile("kczk.jpg");
            //towrzenie samochodów
            car1 = Image.FromFile("car1.jpg");
            car2 = Image.FromFile("car2.jpg");
            car3 = Image.FromFile("car3.jpg");
            car4 = Image.FromFile("car4.jpg");

            //zmiana wielkości obrazu
            kaczka = ResizeImage(kaczka,new Size(40, 40));
            //zmiana wielkości samochodów
            car1 = ResizeImage(car1, new Size(40, 40));
            car2 = ResizeImage(car2, new Size(40, 40));
            car3 = ResizeImage(car3, new Size(40, 40));
            car4 = ResizeImage(car4, new Size(40, 40));

            //konwersja gracza
            Bitmap BM_kaczka = new Bitmap(kaczka, new Size(40, 40));
            //konwersja samochodów
            Bitmap BM_car1 = new Bitmap(car1, new Size(40, 40));
            Bitmap BM_car2 = new Bitmap(car2, new Size(40, 40));
            Bitmap BM_car3 = new Bitmap(car3, new Size(40, 40));
            Bitmap BM_car4 = new Bitmap(car4, new Size(40, 40));

        }
        private static System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, Size size)
        {
            // Get the image current width
            int sourceWidth = imgToResize.Width;
            // Get the image current height
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            // Calculate width and height with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            nPercent = Math.Min(nPercentW, nPercentH);
            // New Width and Height
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
        private bool IsColliding(Image imageA, Point positionA, Image imageB, Point positionB)
        {
            Rectangle rectA = new Rectangle(positionA, imageA.Size);
            Rectangle rectB = new Rectangle(positionB, imageB.Size);

            using (GraphicsPath pathA = new GraphicsPath())
            using (GraphicsPath pathB = new GraphicsPath())
            {
                pathA.AddRectangle(rectA);
                pathB.AddRectangle(rectB);

                Region regionA = new Region(pathA);
                Region regionB = new Region(pathB);

                regionA.Intersect(regionB);

                // Użyj Graphics obiektu związanej z konkretnym kontrolką lub elementem, jeśli to jest kontrolka
                using (Graphics graphics = Graphics.FromImage(new Bitmap(1, 1)))
                {
                    return !regionA.IsEmpty(graphics);
                }
            }
        }
        private void TimerEvent(object sender, EventArgs e)
        {


            if (goLeft && positionX > 0)
            {
                positionX -= speed;
            }
            if (goRight && positionX + width < this.ClientSize.Width)
            {
                positionX += speed; 
            }
            if(goUp && positionY > 0)
            {
                positionY -= speed;
            }
            if(goDown && positionY + height < this.ClientSize.Height)
            {
                positionY += speed;
            }
            if(x1 > 1500)
            {
                x1 = -200;
            }
            if(y1 == -1400){
                y1 = 0;
            }
            //car1
            if(IsColliding(kaczka, main, car1,prawo2))
            {
                positionX = 100;
                positionY = 100;
            }
            //car2
            if (IsColliding(kaczka, main, car2, lewo2))
            {
                positionX = 100;
                positionY = 100;
            }
            //car3
            if (IsColliding(kaczka, main, car3, prawo1))
            {
                positionX = 100;
                positionY = 100;
            }
            //car4
            if (IsColliding(kaczka, main, car4, lewo1))
            {
                positionX = 100;
                positionY = 100;
            }
            main = new Point(positionX, positionY);
            lewo1 = new Point(0+x1, 233);
            prawo1 = new Point(800+y1, 178);
            lewo2 = new Point(-400+x1, 233);
            prawo2 = new Point(1200+y1, 178);
            x1+= 6;
            y1-= 2;
                this.Invalidate();

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            else if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            else if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
        }

        private void FromPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            //konwersja gracza
            Bitmap BM_kaczka = new Bitmap(kaczka, new Size(40, 40));
            //konwersja samochodów
            Bitmap BM_car1 = new Bitmap(car1, new Size(40, 40));
            Bitmap BM_car2 = new Bitmap(car2, new Size(40, 40));
            Bitmap BM_car3 = new Bitmap(car3, new Size(40, 40));
            Bitmap BM_car4 = new Bitmap(car4, new Size(40, 40));
            //gracz
            Canvas.DrawImage(kaczka, positionX,positionY,height,width);

            //samochody pokazanie
            e.Graphics.DrawImage(BM_car1, prawo2);
            e.Graphics.DrawImage(BM_car2, lewo2);
            e.Graphics.DrawImage(BM_car3, prawo1);
            e.Graphics.DrawImage(BM_car4, lewo1);
        }
    }
}
