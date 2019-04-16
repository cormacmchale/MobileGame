using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace test
{
    class Imager
    {
        //function that adds an image to an image object
        public ImageSource AddImage(string imageName)
        {
            var assembly = typeof(MainPage);
            return ImageSource.FromResource("test.Assets." + imageName, assembly);
        }
    }
}
