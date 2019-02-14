using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace test
{
    class Imager
    {
        //function to save image to a variable to use
        public void Test()
        {
            Debug.WriteLine("test");
        }

        //function that adds an image to an object
        public Image AddImage()
        {
            var ImageToAdd = new Image()
            {
                Source = ImageSource.FromFile("test/Assets/player.png")
            };
            return ImageToAdd;
        }
    }
}
