﻿using System;
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
        Image test = new Image()
        {
          HeightRequest = 15,
          WidthRequest = 15
        };
        Image mainBackground = new Image();

        public MainPage()
        {
            InitializeComponent();
            InitilizeGame();
        }

        private void InitilizeGame()
        {
            //add image to game
            test.Source = getImage.AddImage("player.gif");
            test.SetValue(Grid.RowProperty, 1);
            test.SetValue(Grid.ColumnProperty, 1);
            test.Scale = 0.5;
            mainBackground.SetValue(Grid.RowProperty, 1);
            mainBackground.SetValue(Grid.ColumnProperty, 1);
            mainBackground.Source = getImage.AddImage("space.gif");
            mainBackground.Scale = 3;
            //Main.Children.Add(mainBackground);
            Main.Children.Add(test);

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
        //fired in thread.. keep small
        private void movingGame()
        {
            if (GameObject.TranslationX >= 500)
            {
                GameObject.TranslationX = -500;
            }
            test.TranslationX += movementX;
            test.TranslationY += movementY;
            GameObject.TranslationX++;
        }

        #region button press logic (translation and rotation)
        //change movement
        private void UPMOVE(object sender, EventArgs e)
        {
            movementY = -2;
            movementX = 0;
        }
        //change movement
        private void DOWNMOVE(object sender, EventArgs e)
        {
            movementY = +2;
            movementX = 0;
        }
        //change movement
        private void LEFT(object sender, EventArgs e)
        {
            movementX = -2;
            movementY = 0;
        }
        //change movement
        private void RIGHT(object sender, EventArgs e)
        {
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
            movementX = -2;
            movementY = -2;
            //unsure what to do here
            //test.RotationX = 20;
        }
        private void DOWNLEFTMOVE(object sender, EventArgs e)
        {
            movementX = -2;
            movementY = +2;
        }

        private void UPRIGHTMOVE(object sender, EventArgs e)
        {
            movementX = +2;
            movementY = -2;
        }
        private void DOWNRIGHTMOVE(object sender, EventArgs e)
        {
            movementX = +2;
            movementY = +2;
        }
        #endregion


    }
}
