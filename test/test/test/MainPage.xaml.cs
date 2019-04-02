using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
namespace test
{
    public partial class MainPage : ContentPage
    {
        //keep track of game objects
        private ObservableCollection<Image> gameObjects = new ObservableCollection<Image>();
        //10 milliseconds is optimal for player movement speed
        Timer t = new Timer(100);
        Timer collisionTimer = new Timer(30);
        Timer translateTimer = new Timer(3000);
        //variables for movement and collision detection
        static int movementX = 0;
        static int movementY = 0;
        static double distance = 0;
        //class for retrieving images for whatever object needs and image
        Imager getImage = new Imager();
        //main player
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
            translateTimer.Start();
            //add all images first
            addImages();
            //add buttons
            addbuttons();
        }

        private void addbuttons()
        {
            //throw new NotImplementedException();
            Button b = new Button();
            b.SetValue(Grid.RowProperty, 8);
            b.SetValue(Grid.ColumnProperty, 8);
            b.SetValue(Grid.ColumnSpanProperty, 3);
            b.SetValue(Grid.RowSpanProperty, 3);
            b.SetValue(Button.OpacityProperty, 0.5);
            Main.Children.Add(b);

            b.Clicked += B_Clicked;
        }

        //button testing
        private void B_Clicked(object sender, EventArgs e)
        {
            addImages();
            //throw new NotImplementedException();
        }

        //runs like oninit()
        protected override void OnAppearing()
        {
            base.OnAppearing(); 
        }
        //method will create and add a game object that looks like an asteroid to a list
        //can just use the Image object as Game Objects
        private void addImages()
        {
            //throw new NotImplementedException();
            Image asteroid = new Image();
            asteroid.Source = getImage.AddImage("newAsteroid.png");
            //asteroid.Scale = .5;
            //asteroid.SetValue(Grid.RowProperty, 1);
            //asteroid.SetValue(Grid.ColumnProperty, 1);
            asteroid.TranslationX = 300;
            asteroid.TranslationY = 300;
            gameObjects.Add(asteroid);
            Main.Children.Add(asteroid);
        }
        private void TranslateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
          
            Debug.Write("move");
            Device.BeginInvokeOnMainThread( () =>
            {
                //move the pieces
                MoveGameObjects();
                //detect collisions
            }
            );
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
        #region - timer Methods
        private void collisionDetection()
        {
           if (gameObjects.Count > 0)
           {
                foreach (var GameObject in gameObjects)
                {
                    distance = Math.Sqrt(((GameObject.TranslationX - playerShip.TranslationX) * (GameObject.TranslationX - playerShip.TranslationX))
                    + ((GameObject.TranslationY - playerShip.TranslationY) * (GameObject.TranslationY - playerShip.TranslationY)));
                    // Debug.WriteLine("Asteroid X="+asteroid.TranslationX+" "+"Player X="+playerShip.TranslationX);
                    // Debug.WriteLine("Asteroid Y=" + asteroid.TranslationY + " " + "Player Y=" + playerShip.TranslationY);
                    //Debug.WriteLine(distance);
                    //Debug.WriteLine(GameObject.X + " " + playerShip.X);
                    if (distance <= 40)
                    {
                        //Debug.WriteLine(distance);
                        Debug.WriteLine("Collision");
                        if (Main.BackgroundColor == Color.Black)
                        {
                            Main.BackgroundColor = Color.White;
                        }
                        else
                        {
                            Main.BackgroundColor = Color.Black;
                        }
                        //pop new page onto the stack
                        changePage();
                        return;

                    }
                }
           }
        }

        private async void changePage()
        {
            //pass score into save file
            //new page should read file
            await Navigation.PushAsync(new HighScoreReplay());
        }

        private void MoveGameObjects()
        {
            foreach (var GameObject in gameObjects)
            {
                //Debug.Write("move");
                Random r = new Random();
                int moveX = r.Next(0, 200);
                int moveY = r.Next(0, 200);
                GameObject.TranslateTo(moveX, moveY, 2000);
            }
        }
        private void movingGame()
        {
            playerShip.TranslationX += movementX;
            playerShip.TranslationY += movementY;
        }
        #endregion
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
            //addAsteroid();
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
