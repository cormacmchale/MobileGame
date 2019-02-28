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
        public ImageSource AddImage(string imageName)
        {
            var assembly = typeof(MainPage);
            return ImageSource.FromResource("test.Assets." + imageName, assembly);
        }
    }
}
