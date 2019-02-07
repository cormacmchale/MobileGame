using System;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;

namespace test
{
    public partial class MainPage : ContentPage
    {
        static int test;
        Timer t = new Timer(70);
        static int movement;

        //Avoid this object
        BoxView b = new BoxView
        {
            HeightRequest = 10,
            WidthRequest = 10,
            Color = Color.Black,
        };
        public MainPage()
        {
            InitializeComponent();
            t.Elapsed += T_Elapsed1;
            t.Start();
            //b.SetValue(Grid.RowProperty, 0);
            //Main.Children.Add(b);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            t.Stop();
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
            movement = 2;
        }
        //change movement
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            movement = -2;
        }
        private  void movingGame()
        {
            // BoxView player = FindByName("Player") as BoxView;
            Debug.WriteLine("Timer");
            test++;
            Player.TranslationX += movement;
            Debug.WriteLine(Player.TranslationX);
            //b.TranslationX += -2;
        }
    }
}
