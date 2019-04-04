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
        scorePosition score = new scorePosition();
        //10 milliseconds is optimal for player movement speed
        Timer movePlayer = new Timer(35);
        Timer collisionTimer = new Timer(30);
        Timer translateTimer = new Timer(3000);
        //variables for movement and collision detection
        static int movementX = 0;
        static int movementY = 0;
        static double distance = 0;
        //class for retrieving images for whatever object needs and image
        Imager getImage = new Imager();
        //main player
        Image playerShip = new Image();
        Image space = new Image();
        public MainPage()
        {
            InitializeComponent();
            InitilizeGame();
            BindingContext = new scorePosition();
        }
        private void InitilizeGame()
        {
            //add image to game
            playerShip.Source = getImage.AddImage("player.gif");
            //space.Source = getImage.AddImage("newSpace.png");
            //space.SetValue(Grid.ColumnSpanProperty, 16);
            //space.SetValue(Grid.RowSpanProperty, 9);
            //space.TranslationX -= 120;

            //playerShip.SetValue(Grid.RowProperty, 1);
            //playerShip.SetValue(Grid.ColumnProperty, 1);
            //playerShip.Scale = 0.5;
            //playerShip.TranslationX = 20;
            //playerShip.TranslationY = 20;

            //Main.Children.Add(space);
            Main.Children.Add(playerShip);

            //set up timers
            movePlayer.Elapsed += T_Elapsed1;
            collisionTimer.Elapsed += T_Elapsed2;
            translateTimer.Elapsed += TranslateTimer_Elapsed;
            //add all images first

                    //addImages();

            //add buttons
            addbuttons();
            //playerScore();
        }

        private void playerScore()
        {
            //bindScore.BindingContext = new scorePosition();
            //PlayerScore.BindingContext = score.Score;
        }

        private void addbuttons()
        {
            //throw new NotImplementedException();
            Button DownRight = new Button();
            DownRight.SetValue(Grid.RowProperty, 6);
            DownRight.SetValue(Grid.ColumnProperty, 6);
            DownRight.SetValue(Grid.ColumnSpanProperty, 3);
            DownRight.SetValue(Grid.RowSpanProperty, 3);
            DownRight.SetValue(OpacityProperty, 0.0);
            Main.Children.Add(DownRight);
            DownRight.Clicked+=DOWNRIGHTMOVE;

            Button Down = new Button();
            Down.SetValue(Grid.RowProperty, 6);
            Down.SetValue(Grid.ColumnProperty, 3);
            Down.SetValue(Grid.ColumnSpanProperty, 3);
            Down.SetValue(Grid.RowSpanProperty, 3);
            Down.SetValue(OpacityProperty, 0.0);
            Main.Children.Add(Down);
            Down.Clicked +=DOWNMOVE;

            Button DownLeft = new Button();
            DownLeft.SetValue(Grid.RowProperty, 6);
            DownLeft.SetValue(Grid.ColumnProperty, 0);
            DownLeft.SetValue(Grid.ColumnSpanProperty, 3);
            DownLeft.SetValue(Grid.RowSpanProperty, 3);
            DownLeft.SetValue(OpacityProperty, 0.0);
            Main.Children.Add(DownLeft);
            DownLeft.Clicked += DOWNLEFTMOVE;

            Button Right = new Button();
            Right.SetValue(Grid.RowProperty, 3);
            Right.SetValue(Grid.ColumnProperty, 6);
            Right.SetValue(Grid.ColumnSpanProperty, 3);
            Right.SetValue(Grid.RowSpanProperty, 3);
            Right.SetValue(OpacityProperty, 0.0);
            Main.Children.Add(Right);
            Right.Clicked += RIGHT;

            Button Left = new Button();
            Left.SetValue(Grid.RowProperty, 3);
            Left.SetValue(Grid.ColumnProperty, 0);
            Left.SetValue(Grid.ColumnSpanProperty, 3);
            Left.SetValue(Grid.RowSpanProperty, 3);
            Left.SetValue(OpacityProperty, 0.0);
            Main.Children.Add(Left);
            Left.Clicked += LEFT;

            Button TopLeft = new Button();
            TopLeft.SetValue(Grid.RowProperty, 0);
            TopLeft.SetValue(Grid.ColumnProperty, 0);
            TopLeft.SetValue(Grid.ColumnSpanProperty, 3);
            TopLeft.SetValue(Grid.RowSpanProperty, 3);
            TopLeft.SetValue(OpacityProperty, 0.0);
            Main.Children.Add(TopLeft);
            TopLeft.Clicked += UPLEFTMOVE;

            Button Top = new Button();
            Top.SetValue(Grid.RowProperty, 0);
            Top.SetValue(Grid.ColumnProperty, 3);
            Top.SetValue(Grid.ColumnSpanProperty, 3);
            Top.SetValue(Grid.RowSpanProperty, 3);
            Top.SetValue(OpacityProperty, 0.0);
            Main.Children.Add(Top);
            Top.Clicked += UPMOVE;

            Button TopRight = new Button();
            TopRight.SetValue(Grid.RowProperty, 0);
            TopRight.SetValue(Grid.ColumnProperty, 6);
            TopRight.SetValue(Grid.ColumnSpanProperty, 3);
            TopRight.SetValue(Grid.RowSpanProperty, 3);
            TopRight.SetValue(OpacityProperty, 0.0);
            Main.Children.Add(TopRight);
            TopRight.Clicked += UPRIGHTMOVE;
        }

        //button testing
        //private void B_Clicked(object sender, EventArgs e)
        //{
        //    addImages();
            //throw new NotImplementedException();
        //}

        //runs like oninit()
        //start and stop timer just in case they continue to run after page navigation
        protected override void OnAppearing()
        {
            base.OnAppearing();
            collisionTimer.Start();
            movePlayer.Start();
            translateTimer.Start();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            movePlayer.Stop();
            translateTimer.Stop();
            //reset Game function
            resetGame();
        }
        //this method should clear all the game objects and reset the player to where they were in main page
        //maybe runs better when the page appears
        private void resetGame()
        {
            playerShip.TranslationX = 0;
            playerShip.TranslationY = 0;
            movementX = 0;
            movementY = 0;
            //make all of the objects invisible
            foreach (var GameObject in gameObjects)
            {
                GameObject.IsVisible = false;
            }
            //then clear the list
            gameObjects.Clear();
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
            int newScore = Convert.ToInt32(score.Score);
            newScore++;
            score.Score = newScore.ToString();
            //Debug.Write("move");
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
            }
            );
            //CoreDispatcher.
        }
        private void T_Elapsed2(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //detect collisions
                collisionDetection();
            }
            );
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
                        //stop the collision timer
                        collisionTimer.Stop();
                        //pop new page onto the stack
                        changePage();
                    }
                }
           }
        }

        private async void changePage()
        {
            //pass score into save file
            //new page should read file
            await Navigation.PushAsync(new HighScoreReplay(Convert.ToInt32(score.Score)));
        }

        private void MoveGameObjects()
        {
            foreach (var GameObject in gameObjects)
            {
                //Debug.Write("move");
                Random r = new Random();
                int moveX = r.Next(-200, 200);
                int moveY = r.Next(-200, 200);
                GameObject.TranslateTo(moveX, moveY, 1700);
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
            gameObjects.Clear();
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
