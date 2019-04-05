using Plugin.DeviceSensors;
using Plugin.DeviceSensors.Shared;
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
        #region- Global Variables
        //keep track of game objects
        private ObservableCollection<Image> gameObjects = new ObservableCollection<Image>();
        //keep track of score.. required for data binding
        scorePosition score = new scorePosition();
        //10 milliseconds is optimal for player movement speed
        Timer movePlayer = new Timer(35);
        Timer collisionTimer = new Timer(30);
        Timer sensorTimer = new Timer(50);
        Timer translateTimer = new Timer(3000);
        //variables for movement and collision detection
        static int movementX = 0;
        static int movementY = 0;
        static double distance = 0;
        //class for retrieving images for whatever object needs and image
        //progamatically useful and nice to experiemnt with c# code
        Imager getImage = new Imager();
        //main player and background
        Image playerShip = new Image();
        //this not working as intended
        Image space = new Image();
        #endregion

        public MainPage()
        {
            InitializeComponent();
            InitilizeGame();
        }
        private void InitilizeGame()
        {
            //add player image to game
            playerShip.Source = getImage.AddImage("player.gif");

            //set up timers
            movePlayer.Elapsed += T_Elapsed1;
            collisionTimer.Elapsed += T_Elapsed2;
            translateTimer.Elapsed += TranslateTimer_Elapsed;
            sensorTimer.Elapsed += SensorTimer_Elapsed;

            //if the accelermoter is available then add the property changed event and start reading.. also start the timer to change movement
            if (CrossDeviceSensors.Current.Accelerometer.IsSupported)
            {
                CrossDeviceSensors.Current.Accelerometer.OnReadingChanged += (s, a) => {
                    //Debug.WriteLine(s);
                    //Debug.WriteLine(a);
                };
                CrossDeviceSensors.Current.Accelerometer.StartReading();
                //log reading from accelerometer for testing
                //VectorReading n = CrossDeviceSensors.Current.Accelerometer.LastReading;
                //Debug.Write(n.X);
                sensorTimer.Start();
            }

            //unsure as to why this wont fill page
            //this now added in xaml
            //Main.Children.Add(space);
            Main.Children.Add(playerShip);

            //add buttons
            addbuttons();
            //databinding Fail
            //playerScore();
        }

        //method will create and add a game object that looks like an asteroid to a list
        //can just use the Image object as Game Objects
        //this method will have to be updated to maybe add images at a random location?
        //not implemented at the moment
        //after movement is finished
        //multiple calls of this method will increase difficulty
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

        #region - Onappearing and disappearing
        //runs like oninit()
        //start and stop timer just in case they continue to run after page navigation
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //loop through collection of images and perform collision detect between the player and those object
            collisionTimer.Start();
            //loop through colection of images and perform movement
            translateTimer.Start();
            //start allowing the player to move
            movePlayer.Start();
        }
        //when the page goes out of scope in terms of UI
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            movePlayer.Stop();
            //reset Game function
            //gets rid of objects without breaking the game
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
        #endregion

        #region- Buttons added programatically
        //buttons here.. Won't need for the device but may need instead of
        //accelermoeter will now call button presses for android
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
        #endregion

        #region - Timer invoke on main thread
        private void TranslateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int newScore = Convert.ToInt32(score.Score);
            newScore++;
            score.Score = newScore.ToString();
            //Debug.Write("move");
            Device.BeginInvokeOnMainThread( () =>
            {
                //move the gameObjects
                MoveGameObjects();
            }
            );
        }
        private void T_Elapsed1(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //move the player
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
        private void SensorTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //detect collisions
                moveShipWithSensor();
            }
            );
        }
        #endregion

        #region - timer Methods
        private void collisionDetection()
        {
           if (gameObjects.Count > 0)
           {
                try
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
                            //stop both timers - android issue maybe?
                            translateTimer.Stop();
                            collisionTimer.Stop();
                            //pop new page onto the stack
                            changePage();
                        }
                    }
                }
                //error handling for android??
                catch
                {
                    //Debug.WriteLine(distance);
                    Debug.WriteLine("Collision");
                    //stop both timers - android issue maybe?
                    translateTimer.Stop();
                    collisionTimer.Stop();
                    //pop new page onto the stack
                    changePage();
                }
           }
        }
        //fired in the sensor reading timer
        private void moveShipWithSensor()
        {
            VectorReading n = CrossDeviceSensors.Current.Accelerometer.LastReading;
            tiltMoveShip(n.X);
        }
        //gets the value from the reading and alters movement accordingly
        private void tiltMoveShip(double X)
        {
            //will have to add Large switch statement here for movement??
            //will research better way to do this
            //Debug.WriteLine(X);
            if (X > 6)
            {
                movementX = -2;
                movementY = 0;
                addImages();
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
            //addImages();
            playerShip.RotationY = 12;
            playerShip.Rotation = 0;
            movementX = 2;
            movementY = 0;
        }
        private void STOP(object sender, EventArgs e)
        {
            //addAsteroid();
            //gameObjects.Clear();
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
