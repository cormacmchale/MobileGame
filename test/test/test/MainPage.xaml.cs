using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;

namespace test
{
    public partial class MainPage : ContentPage
    {

        int test = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            test++;
            Entry entry = FindByName("changeNum") as Entry;
            entry.Text = test.ToString();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            BoxView player = FindByName("Player") as BoxView;
            player.TranslationX+=-4;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            BoxView player = FindByName("Player") as BoxView;
            player.TranslationX+=4;
        }
    }
}
