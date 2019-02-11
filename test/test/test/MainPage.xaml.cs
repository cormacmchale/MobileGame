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
        static int movement = 0;
        static int movementY = 0;
        static double distance = 0;

        public MainPage()
        {
            InitializeComponent();
            t.Elapsed += T_Elapsed1;
            t.Start();
            GameObject.TranslationX = -200;
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
            distance = Math.Sqrt(((GameObject.TranslationX - Player.TranslationX) * (GameObject.TranslationX - Player.TranslationX)) 
            + ((GameObject.TranslationY - Player.TranslationY) * (GameObject.TranslationY - Player.TranslationY)));
            // BoxView player = FindByName("Player") as BoxView;
            if (distance<=3)
            {
                Debug.WriteLine("Collision");
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
