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
        Timer t = new Timer(10);
        Timer collisionTimer = new Timer(50);
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



        public MainPage()
        {
            InitializeComponent();
            InitilizeGame();
        }

        private void InitilizeGame()
        {
            //add image to game
            playerShip.Source = getImage.AddImage("player.gif");
            playerShip.SetValue(Grid.RowProperty, 1);
            playerShip.SetValue(Grid.ColumnProperty, 1);
            playerShip.Scale = 0.5;
            Main.Children.Add(playerShip);

            //set up timers
            t.Elapsed += T_Elapsed1;
            collisionTimer.Elapsed += T_Elapsed2;
            t.Start();
            collisionTimer.Start();

            addAsteroid();
           
        }

        //method will create and add a game object that looks like an asteroid to a list
        //can just use the Image object as Game Objects
        private void addAsteroid()
        {
            //throw new NotImplementedException();
            Image asteroid = new Image();
            asteroid.Source = getImage.AddImage("asteroidTwo.png");
            Main.Children.Add(asteroid);
            asteroid.Scale = .2;
            asteroid.SetValue(Grid.RowProperty, 1);
            asteroid.SetValue(Grid.ColumnProperty, 1);
            asteroid.TranslationX = 50;
            asteroid.TranslationY = 50;
            gameObjects.Add(asteroid);
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
            if (gameObjects.Count > 0)
            {
                foreach (var GameObject in gameObjects)
                {
                    distance = Math.Sqrt(((GameObject.TranslationX - playerShip.TranslationX) * (GameObject.TranslationX - playerShip.TranslationX))
                    + ((GameObject.TranslationY - playerShip.TranslationY) * (GameObject.TranslationY - playerShip.TranslationY)));
                    Debug.WriteLine(distance);
                    if (distance <= 3)
                    {
                        //Debug.WriteLine(distance);
                        Debug.WriteLine("Collision");
                        Main.BackgroundColor = Color.Black;
                    }
                }
            }
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
