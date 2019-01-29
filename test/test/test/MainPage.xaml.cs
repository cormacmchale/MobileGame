using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void addCanvas()
        {
            StackLayout entry = FindByName("MainStack") as StackLayout;
            Canvas game = new Canvas();
            //entry.Children.Add(game);
        }
    }
}
