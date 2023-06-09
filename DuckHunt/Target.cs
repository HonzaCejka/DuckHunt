using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DuckHunt
{
    public class Target
    {
        private MediaPlayer mediaPlayer;
        public int size { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public int Shots { get; set; }
        public int Points { get; set; }
        public Grid maingrid { get; set; }        
        public int WinWidth { get; set; }
        public int WinHeight { get; set; }
        public bool trefa { get; set; }
        public Canvas MyCanvas { get; set; }
        public TextBlock Score { get; set; }
        public TextBlock Shot { get; set; }

        DispatcherTimer timer = new DispatcherTimer();
        public Target(Canvas canvas, Grid grid,double winw,double winh,TextBlock score,TextBlock shot) 
        {
            Shot = shot;
            Score = score;
            Shots = 15;
            MyCanvas = canvas;
            UpdateSize(winw,winh);
            maingrid = grid;            
            timer.Tick += Timer_Tick;
            SetTimer();
            showtargets();
            mediaPlayer = new MediaPlayer();
            
        }
        
        public void prebit()
        {
            mediaPlayer.Open(new Uri("sounds/reload.mp3", UriKind.Relative));
            mediaPlayer.Play();
            Shots = 0;
            for (int i = 0; i < 15; i++)
            {                
                Shots++;
                Shot.Text = "Shots: " + Shots;                
            }
        }
        public void vystrel()
        {
            if (trefa != true)
            {
                mediaPlayer.Open(new Uri("sounds/Shot.mp3", UriKind.Relative));
                mediaPlayer.Play();
            }            
            //zvuk strtely
            if ((Shots-1)>=0)
            {
                Shots -= 1;
                Shot.Text = "Shots: "+ Shots;
            }
            trefa = false;
        }

        public void SetTimer()
        {
            Random random= new Random();
            timer.Interval = TimeSpan.FromSeconds(random.Next(2, 10));            
            timer.Start();
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {                        
            MyCanvas.Children.Clear();            
            timer.Stop();
            showtargets();
        }
        public void UpdateSize(double winw,double winh)
        {
            WinWidth = (int)Math.Round(winw, 0);
            WinHeight = (int)Math.Round(winh, 0)-38;
            if (WinHeight == 0||WinHeight==0)
            {
                WinWidth = 800;
                WinHeight = 450-38;
            }
        }                
        public void showtargets()
        {
            int size = Size(50, 200);
            int[] pos = Position(size);

            Ellipse elr = new Ellipse();            
            elr.Width = size;
            elr.Height = size;
            elr.Stretch = Stretch.Fill;            
            elr.Fill = Brushes.Red;
            elr.MouseDown += trefen;            
            Canvas.SetLeft(elr, pos[0]);
            Canvas.SetTop(elr, pos[1]);
            MyCanvas.Children.Add(elr);

            Ellipse elw = new Ellipse();
            int size2 = size/2;
            elw.Width = size2;
            elw.Height = size2;
            elw.Stretch = Stretch.Fill;
            elw.Fill = Brushes.White;
            elw.MouseDown += trefen;            
            Canvas.SetLeft(elw, pos[0]+size/4);
            Canvas.SetTop(elw, pos[1]+size/4);
            MyCanvas.Children.Add(elw);

            Ellipse elrv = new Ellipse();
            int size3 = size2 / 2;
            elrv.Width = size3;
            elrv.Height = size3;
            elrv.Stretch = Stretch.Fill;
            elrv.Fill = Brushes.Red;
            elrv.MouseDown += trefen;
            Canvas.SetLeft(elrv, pos[0] + size/2-size3/2);
            Canvas.SetTop(elrv, pos[1] + size/2 - size3 / 2);
            MyCanvas.Children.Add(elrv);
        }
        public async void trefen(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Open(new Uri("sounds/Shot.mp3", UriKind.Relative));
            mediaPlayer.Play();
            await Task.Delay(600);
            mediaPlayer.Open(new Uri("sounds/hit.mp3", UriKind.Relative));
            mediaPlayer.Play();
            await Task.Delay(500);
            if (Shots >0)
            {
                MyCanvas.Children.Clear();
                Points += 5;
                Score.Text = "Points: " + Points.ToString();
                if (Points == 200)
                {
                    MessageBox.Show("You Win :)");
                    Application.Current.Shutdown();
                }
                SetTimer();
                showtargets();
            }
            
            else
            {
                MessageBox.Show("přebij");
            }
            trefa = true;
        }
        public int Size(int s,int e)
        {
            Random random = new Random();
            int size = random.Next(s, e);
            return size;
        }
        public int[] Position(int size)
        {
            int[] pos = new int[2];           
            Random random= new Random();
            int x;
            int y;            
            try
            {
                x = random.Next(size/2, (WinWidth - size*2));
                y = random.Next(size/2, (WinHeight - size*2));
                pos[0] = x;
                pos[1] = y;
            }
            catch {}                  
            return pos;
        }
    }
}
