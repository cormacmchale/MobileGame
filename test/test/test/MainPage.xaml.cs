using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;

namespace test
{
    public partial class MainPage : ContentPage
    {
        private List<Image> gameObjects = new List<Image>();
        //10 milliseconds is optimal for player movement speed
        Timer t = new Timer(50);
        Timer collisionTimer = new Timer(50);
        Timer translateTimer = new Timer(2000);
        //variables for movement and collision detection
        static int movementX = 0;
        static int movementY = 0;
        static double distance = 0;
        //class for retrieving images for whatever object needs and image
        Imager getImage = new Imager();
        Image playerShip = new Image()
        {
          HeightRequest = 15,
          WidthRequest = 15
        };
        Image asteroid;
        public MainPage()
        {
            InitializeComponent();
            InitilizeGame();
        }

        private void InitilizeGame()
        {
            //add image to game
            playerShip.Source = getImage.AddImage("player.gif");
            //playerShip.SetValue(Grid.RowProperty, 1);
            //playerShip.SetValue(Grid.ColumnProperty, 1);
            playerShip.Scale = 0.5;
            //playerShip.TranslationX = 20;
            //playerShip.TranslationY = 20;
            Main.Children.Add(playerShip);
            //set up timers
            t.Elapsed += T_Elapsed1;
            collisionTimer.Elapsed += T_Elapsed2;
            translateTimer.Elapsed += TranslateTimer_Elapsed;
            collisionTimer.Start();
            t.Start();
            addAsteroid();

        }
        //runs like oninit()
        protected override void OnAppearing()
        {
            base.OnAppearing(); 
        }

        //method will create and add a game object that looks like an asteroid to a list
        //can just use the Image object as Game Objects
        private void addAsteroid()
        {
            //throw new NotImplementedException();
            asteroid = new Image() {
                HeightRequest = 2,
                WidthRequest = 2
            };
            asteroid.Source = getImage.AddImage("newAsteroid.png");
            //asteroid.Scale = .5;
            //asteroid.SetValue(Grid.RowProperty, 1);
            //asteroid.SetValue(Grid.ColumnProperty, 1);
            asteroid.TranslationX = 50;
            asteroid.TranslationY = 50;
            Main.Children.Add(asteroid);

            //gameObjects.Add(asteroid);
        }
        private void TranslateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //move the pieces
                MoveGameObjects();
                //detect collisions
            }
            );
        }

        private void MoveGameObjects()
        {
            throw new NotImplementedException();
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
           //if (gameObjects.Count > 0)
           // {
               // foreach (var GameObject in gameObjects)
               // {
                    distance = Math.Sqrt(((asteroid.TranslationX - playerShip.TranslationX) * (asteroid.TranslationX - playerShip.TranslationX))
                    + ((asteroid.TranslationY - playerShip.TranslationY) * (asteroid.TranslationY - playerShip.TranslationY)));
                    Debug.WriteLine("Asteroid X="+asteroid.TranslationX+" "+"Player X="+playerShip.TranslationX);
                    Debug.WriteLine("Asteroid Y=" + asteroid.TranslationY + " " + "Player Y=" + playerShip.TranslationY);
            Debug.WriteLine(distance);
            //Debug.WriteLine(GameObject.X + " " + playerShip.X);
            if (distance <= 5)
                    {
                        //Debug.WriteLine(distance);
                        Debug.WriteLine("Collision");
                        Main.BackgroundColor = Color.Black;
                    }
              //  }
           // }
        }
        //fired in thread.. keep small
        private void movingGame()
        {
            playerShip.TranslationX += movementX;
            playerShip.TranslationY += movementY;
            //add translateTo method for moving the gameObects that the player will have to avoid
        }

        #region button press logic (translation and rotation)
        private void UPMOVE(object sender, EventArgs e)
        {
            playerShip.RotationX = 10;
            playerShip.Rotation = 0;    
            movementY = -2;
            movementX = 0;
        }
        private void DOWNMOVE(object sender, EventArgs e)
        {
            playerShip.RotationX = -10;
            playerShip.Rotation = 0;
            movementY = +2;
            movementX = 0;
        }
        private void LEFT(object sender, EventArgs e)
        {
            playerShip.RotationY = -12;
            playerShip.Rotation = 0;
            movementX = -2;
            movementY = 0;
        }
        private void RIGHT(object sender, EventArgs e)
        {
            playerShip.RotationY = 12;
            playerShip.Rotation = 0;
            movementX = 2;
            movementY = 0;
        }
        private void STOP(object sender, EventArgs e)
        {
            movementX = 0;
            movementY = 0;
        }
        private void UPLEFTMOVE(object sender, EventArgs e)
        {
            playerShip.Rotation = -15;
            movementX = -2;
            movementY = -2;
        }
        private void DOWNLEFTMOVE(object sender, EventArgs e)
        {
            playerShip.Rotation = -15;
            movementX = -2;
            movementY = +2;
        }

        private void UPRIGHTMOVE(object sender, EventArgs e)
        {
            playerShip.Rotation = 15;
            movementX = +2;
            movementY = -2;
        }
        private void DOWNRIGHTMOVE(object sender, EventArgs e)
        {
            playerShip.Rotation = 15;
            movementX = +2;
            movementY = +2;
        }
        #endregion


    }
}
