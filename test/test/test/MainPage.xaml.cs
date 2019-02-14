using System;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;

namespace test
{
    public partial class MainPage : ContentPage
    {
        //10 milliseconds is optimal for player movement speed
        Timer t = new Timer(10);
        Timer collisionTimer = new Timer(50);
        //variables for movement and collision detection
        static int movementX = 0;
        static int movementY = 0;
        static double distance = 0;
        Imager getImage = new Imager();

        public MainPage()
        {
            InitializeComponent();
            InitilizeGame();
        }

        private void InitilizeGame()
        {
            //add image to game
            Image test = new Image();
            test = getImage.AddImage();
            test.HeightRequest = 50;
            test.WidthRequest = 50;
            test.SetValue(Grid.RowProperty, 1);
            test.SetValue(Grid.ColumnProperty, 1);
     
            //set up game
            t.Elapsed += T_Elapsed1;
            collisionTimer.Elapsed += T_Elapsed2;
            t.Start();
            collisionTimer.Start();       
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
            //Debug.WriteLine(distance);
            // BoxView player = FindByName("Player") as BoxView;
            if (distance <= 3)
            {
                Debug.WriteLine("Collision");
            }
        }
        //change movement
        private void Button_Clicked(object sender, EventArgs e)
        {
            movementY = -2;
            movementX = 0;
        }
        //change movement
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            movementY = +2;
            movementX = 0;
        }
        //change movement
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            movementX = -2;
            movementY = 0;
        }
        //change movement
        private void Button_Clicked_3(object sender, EventArgs e)
        {
            movementX = 2;
            movementY = 0;
        }

        //fired in thread.. keep small
        private void movingGame()
        {
            if (GameObject.TranslationX >= 500)
            {
                GameObject.TranslationX = -500;
            }
            Player.TranslationX += movementX;
            Player.TranslationY += movementY;
            GameObject.TranslationX++;
        }
    }
}
