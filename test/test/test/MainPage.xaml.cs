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
        //timers used to make the game dynamic
        //10 milliseconds is optimal for player movement speed (35 is less dodgy)
        //every time this fires the players position is updated
        Timer movePlayer = new Timer(35);
        //collision detection is run everytime this timer fires
        Timer collisionTimer = new Timer(30);
        //this is fired to track the orientation of the phone
        Timer sensorTimer = new Timer(50);
        //this moves all of the game objects randomly
        Timer translateTimer = new Timer(3000);
        //variables for movement and collision detection
        static int movementX = 0;
        static int movementY = 0;
        //all objects working off one variable - this works
        static double distance = 0;
        //class for retrieving images for whatever object needs and image
        //progamatically useful and nice to experiemnt with c# code
        Imager getImage = new Imager();
        //main player and background
        Image playerShip = new Image();
        //these options will make the png stretch over the grid fully
        //regardless of platform- much nicer for the UI
        Image space = new Image
        {
            Aspect = Aspect.AspectFill,
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand         
        };
        //RNG for enhanced gameplay experience
        //used with gameObject Manipulation - instantiation and movement
        Random r = new Random();
        Random asteroidStartingPoint = new Random();
        //used for managing keep player on screen on any platform
        static double gridHeight;
        static double gridWidth;
        #endregion

        public MainPage()
        {
            InitializeComponent();
            InitilizeGame();
        }
        private void InitilizeGame()
        {
            UI();
            setUpEnvoirnment();
            playerScore();
            startTimers();
        }

        #region - Game setup and UI implementation
        private void UI()
        {
            //add player image and background to game
            //needs to be added before buttons (UWP)
            playerShip.Source = getImage.AddImage("player.gif");
            space.Source = getImage.AddImage("backGround.png");
            space.SetValue(Grid.ColumnSpanProperty, 9);
            space.SetValue(Grid.RowSpanProperty, 9);
            Main.Children.Add(space);
            Main.Children.Add(playerShip);
            Main.Children.Add(space);
            Main.Children.Add(playerShip);
        }
        private void startTimers()
        {
            //set up timers
            //the methods added to the timers here are the methods that fire everytime the timer fires
            movePlayer.Elapsed += T_Elapsed1;
            collisionTimer.Elapsed += T_Elapsed2;
            translateTimer.Elapsed += TranslateTimer_Elapsed;
            sensorTimer.Elapsed += SensorTimer_Elapsed;
        }
        //implement Data Binding here
        private void playerScore()
        {
            //binding so the player can see their score
            scoreTracker.BindingContext = score;
        }
        private void setUpEnvoirnment()
        {
            //set up envoirement to play Games
            //always add buttons in case no accelermoeter or device is different from android or UWP
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    //if the accelermoter is available then add the property changed event and start reading.. also start the timer to change movement
                    if (CrossDeviceSensors.Current.Accelerometer.IsSupported)
                    {
                        CrossDeviceSensors.Current.Accelerometer.OnReadingChanged += (s, a) =>
                        {
                            //Debug.WriteLine(s);
                            //Debug.WriteLine(a);
                        };
                        CrossDeviceSensors.Current.Accelerometer.StartReading();
                        //log reading from accelerometer for testing
                        //VectorReading n = CrossDeviceSensors.Current.Accelerometer.LastReading;
                        //Debug.Write(n.X);
                        sensorTimer.Start();
                    }
                    else
                    {
                        //add buttons just in case
                        addbuttons();
                    }
                    break;
                case Device.UWP:
                    //add buttons for UWP users
                    addbuttons();
                    break;
                default:
                    //add buttons just in case
                    addbuttons();
                    break;
            }
        }
        //hopefully this will help keep player on screen regardless of depolyment envoirenment
        public void getGridDimensions()
        {
            //the *9 is to compensate for rows and columns
            //does not work
            gridHeight = Main.Height;
            gridWidth = Main.Width;
        }
        //method will create and add a game object that looks like an asteroid to a list
        //can just use the Image object as Game Objects
        //this method will have to be updated to maybe add images at a random location?
        //multiple calls of this method will increase difficulty
        private void addGameObject()
        {
            Image asteroid = new Image();
            asteroid.Source = getImage.AddImage("newAsteroid.png");
            //get random starting point away from the player
            //change side depending on score at that moment
            if (score.Score % 2 == 0)
            {
                int playerAvoidX = (int)playerShip.TranslationX + 70;
                int playerAvoidY = (int)playerShip.TranslationY + 70;
                int playerAvoidX2 = (int)playerShip.TranslationX + 120;
                int playerAvoidY2 = (int)playerShip.TranslationY + 120;
                asteroid.TranslationX = asteroidStartingPoint.Next(playerAvoidX, playerAvoidX2);
                asteroid.TranslationY = asteroidStartingPoint.Next(playerAvoidY, playerAvoidY2);
            }
            else
            {
                int playerAvoidX = (int)playerShip.TranslationX - 70;
                int playerAvoidY = (int)playerShip.TranslationY - 70;
                int playerAvoidX2 = (int)playerShip.TranslationX - 120;
                int playerAvoidY2 = (int)playerShip.TranslationY - 120;
                asteroid.TranslationX = asteroidStartingPoint.Next(playerAvoidX2, playerAvoidX);
                asteroid.TranslationY = asteroidStartingPoint.Next(playerAvoidY2, playerAvoidY);
            }
            gameObjects.Add(asteroid);
            Main.Children.Add(asteroid);
        }
        #endregion

        #region - Onappearing and disappearing
        //runs like oninit()
        //start and stop timer just in case they continue to run after page navigation
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //reset Game function
            //gets rid of objects without breaking the game
            resetGame();
            //loop through collection of images and perform collision detect between the player and those object
            collisionTimer.Start();
            //loop through colection of images and perform movement
            translateTimer.Start();
            //start allowing the player to move
            movePlayer.Start();
            //reset the score here
            score.Score = 0;
            //start this timer again - this not being here may have been the reason that app was crashing out sometimes on Android
            if (Device.RuntimePlatform == Device.Android)
            {
                sensorTimer.Start();
            }
        }
        //when the page goes out of scope in terms of UI
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //stop this timer as no need to continue to keep goin in the BackGround
            movePlayer.Stop();
            //stop sensor if active - this not being here may have been the reason that app was crashing out sometimes on Android
            if (Device.RuntimePlatform == Device.Android)
            {
                sensorTimer.Stop();
            }
            //stop all timer just in case? dealing with android crashing issue
            //seems to help, only time will tell.. testing during the day will yeild results
            collisionTimer.Stop();
            translateTimer.Stop();
        }
        //this method should clear all the game objects and reset the player to where they were in main page
        //maybe runs better when the page appears
        private void resetGame()
        {
            //stop the player and move back to start position
            movementX = 0;
            movementY = 0;
            playerShip.TranslationX = 0;
            playerShip.TranslationY = 0;
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
        //buttons here.. Won't need for android, called for UWP
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

            //Debug.Write("move");
            Device.BeginInvokeOnMainThread( () =>
            {
                //every move increase difficulty
                //add the game objects
                addGameObject();
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
                //player score increases at the rate the timer goes off, looks good on the UI
                score.Score++;
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

        #region - Timer Methods
        //not very happy with this but it works as it's supposed to in terms of the UI
        private void collisionDetection()
        {
           if (gameObjects.Count > 0)
           {
                try
                {
                    foreach (var GameObject in gameObjects)
                    {
                        //distance of a line for collision
                        distance = Math.Sqrt(((GameObject.TranslationX - playerShip.TranslationX) * (GameObject.TranslationX - playerShip.TranslationX))
                        + ((GameObject.TranslationY - playerShip.TranslationY) * (GameObject.TranslationY - playerShip.TranslationY)));
                        if (distance <= 40)
                        {
                            //stop both timers - here for error handling
                            //after testing this setup runs optimally
                            resetGame();
                            translateTimer.Stop();
                            collisionTimer.Stop();
                            //pop new page onto the stack
                            changePage();
                        }
                    }
                }
                //error handling for android??
                //changePage called here again will cause the score board to be pushed twice to the stack! 
                //Timer interaction also has strange results in here- only needed to keep the android build running correctly, Java issue maybe?
                catch
                {
                    //android issue?
                    //translateTimer.Stop();
                    //collisionTimer.Stop();
                    //pop new page onto the stack
                    //changePage();
                    //keep android happy
                }
           }
        }

        private async void changePage()
        {
            //pass score into new page
            //new page should read file
            await Navigation.PushAsync(new HighScoreReplay(score.Score));
        }
        //fired in the sensor reading timer
        private void moveShipWithSensor()
        {
            VectorReading n = CrossDeviceSensors.Current.Accelerometer.LastReading;
            //changes movement based on orientation of the phone
            tiltMoveShip(n.X, n.Y);
            //call here to have access to bounds of grid every move
            getGridDimensions();
        }
        //gets the value from the reading and alters movement accordingly
        private void tiltMoveShip(double X, double Y)
        {
            //tidied up - for Up Y has to be a bit smaller to feel more intuitive
            //if user tilts extreme than that stops moevment in the opposite direction
            #region- keep player on screen
            //keep player on screen
            //could be an idea to use main.height here so all phones will work?
            //x position
            if (playerShip.TranslationX < -20)
            { playerShip.TranslationX = (gridWidth+20);}
            if (playerShip.TranslationX > (gridWidth+30))
            { playerShip.TranslationX = -10;}
            //y position
            if (playerShip.TranslationY < -70)
            { playerShip.TranslationY = (gridHeight+65); }
            if (playerShip.TranslationY > (gridHeight+72))
            { playerShip.TranslationY = -60; }
            #endregion

            //left
            if (X > 4)
            { movementX = -2; };
            if (X > 7)
            { movementY = 0; };
            //right
            if (X < -4)
            { movementX = 2; };
            if (X < -7)
            { movementY = 0; };
            //Down
            if (Y > 4)
            { movementY = 2;};
            if (Y > 7)
            { movementX = 0; };
            //UP
            if (Y < -3)
            { movementY = -2;};
            if (Y < -6)
            { movementX = 0; };
            //need to add other movements
            //if (X > 4 && Y > 4)
            //{ movementX = -2; movementY = -2; };
        }

        //loop through the objects onscreen and apply movement
        //tweaking applied for better difficulty curve
        private void MoveGameObjects()
        {
            //this will mean the player must be constantly alert - more rewarding experience
            //random object in the array will head for player directly
            int attackPlayer = r.Next(0,(gameObjects.Count));

                for (int i = 0; i < gameObjects.Count - 1; i++)
                {
                    if (gameObjects[i] == gameObjects[attackPlayer])
                    {
                        gameObjects[i].TranslateTo(playerShip.TranslationX, playerShip.TranslationY, 2000);
                    }
                    else
                    {
                        int moveX = r.Next(0, 500);
                        int moveY = r.Next(0, 800);
                        gameObjects[i].TranslateTo(moveX, moveY, 3000);
                    }
                }
        }
        //apply movement to the player
        private void movingGame()
        {
            playerShip.TranslationX += movementX;
            playerShip.TranslationY += movementY;
        }
        #endregion

        //rotation removed for the moment to fix translation offset issue
        #region button press logic (translation and rotation)
        private void UPMOVE(object sender, EventArgs e)
        {
            //playerShip.RotationX = 10;
            //playerShip.Rotation = 0;    
            movementY = -2;
            movementX = 0;
        }
        private void DOWNMOVE(object sender, EventArgs e)
        {
            //playerShip.RotationX = -10;
            //playerShip.Rotation = 0;
            movementY = +2;
            movementX = 0;
        }
        private void LEFT(object sender, EventArgs e)
        {
            //playerShip.RotationY = -12;
            //playerShip.Rotation = 0;
            movementX = -2;
            movementY = 0;
        }
        private void RIGHT(object sender, EventArgs e)
        {
            //addImages();
            //playerShip.RotationY = 12;
            //playerShip.Rotation = 0;
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
            //playerShip.Rotation = -15;
            movementX = -2;
            movementY = -2;
        }
        private void DOWNLEFTMOVE(object sender, EventArgs e)
        {
            //playerShip.Rotation = -15;
            movementX = -2;
            movementY = +2;
        }
        private void UPRIGHTMOVE(object sender, EventArgs e)
        {
            //playerShip.Rotation = 15;
            movementX = +2;
            movementY = -2;
        }
        private void DOWNRIGHTMOVE(object sender, EventArgs e)
        {
            //playerShip.Rotation = 15;
            movementX = +2;
            movementY = +2;
        }
        #endregion
    }
}
