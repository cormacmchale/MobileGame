using System;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;

namespace test
{
    public partial class MainPage : ContentPage
    {
        static int test;
        //10 milliseconds is optimal for player movement speed
        Timer t = new Timer(10);
        Timer collisionTimer = new Timer(50);
        static int movement = 0;
        static int movementY = 0;
        static double distance = 0;

        public MainPage()
        {
            InitializeComponent();
            t.Elapsed += T_Elapsed1;
            collisionTimer.Elapsed += T_Elapsed2;
            t.Start();
            GameObject.TranslationX = -500;
            //b.SetValue(Grid.RowProperty, 0);
            //Main.Children.Add(b);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //t.Stop();
            movementY = -2;
            test++;
            changeNum.Text = test.ToString();            
        }

        private void B_SizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("SizeChanged");
        }

        private void T_Elapsed1(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //move the pieces
                movingGame();
                //detect collisions
            }
            );
            //CoreDispatcher.
        }
        private void T_Elapsed2(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //move the pieces
                collisionDetection();
                //detect collisions
            }
            );
            //CoreDispatcher.
        }

        private void collisionDetection()
        {
            distance = Math.Sqrt(((GameObject.TranslationX - Player.TranslationX) * (GameObject.TranslationX - Player.TranslationX))
            + ((GameObject.TranslationY - Player.TranslationY) * (GameObject.TranslationY - Player.TranslationY)));
            // BoxView player = FindByName("Player") as BoxView;
            if (distance <= 3)
            {
                Debug.WriteLine("Collision");
            }
        }

        //change movement
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            movement = -2;
        }
        //change movement
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            movement = 2;
        }
        private  void movingGame()
        {
            if (GameObject.TranslationX >= 500)
            {
                GameObject.TranslationX = -500;
            }
                Debug.WriteLine(distance);
                test++;
                Player.TranslationX += movement;
                Player.TranslationY += movementY;
                GameObject.TranslationX++;
                //Debug.WriteLine(Player.TranslationX);
                //b.TranslationX += -2;
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            movementY = 2;
        }
    }
}
