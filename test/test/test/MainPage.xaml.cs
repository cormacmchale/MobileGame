using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;

namespace test
{
    public partial class MainPage : ContentPage
    {
        
        static int test = 0;
        Timer t = new Timer(1000);
        //t.Elapsed+= movingGame();

        public MainPage()
        {
            InitializeComponent();      
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            test++;
            Entry entry = FindByName("changeNum") as Entry;
            entry.Text = test.ToString();
            run();
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            //CoreDispatcher.
            movingGame();
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

        private void movingGame()
        {
            // BoxView player = FindByName("Player") as BoxView;
            Debug.WriteLine("Timer");
            test++;
            BoxView b = new BoxView();
            b.HeightRequest = 50;
            b.WidthRequest = 50;
            //b.Color = Windows.UI.Colors.Black;
            //CoreDispatcher.RunAsync(CoreDispatcherPriority.Normal,
            ///() =>
                //{
    // Your UI update code goes here!
                //});


            //changeNum.Text = test.ToString();
            Player.TranslationX += -4;
            //Entry entry = FindByName("changeNum") as Entry;
            //entry.Text = test.ToString();
        }
        private async void run()
        {
            t.Start();
            t.Elapsed += T_Elapsed;
        }
    }
}
