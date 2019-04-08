using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HighScoreReplay : ContentPage
	{
        List<scorePosition> highScore = new List<scorePosition>();
		public HighScoreReplay (int Score)
		{
			InitializeComponent();
            readScoresFromFile();
            setUpScoreBoard(Score);
		}
        //implementing on disappearing for saving to file
        //after that if read from correct file is used properly than this is finished
        private void readScoresFromFile()
        {
            //Debug.Write("In Method \n");
            highScore = fileReaderHighScores.readInHighScoreList();
            //Debug.Write(highScore[0].Score);
            //throw new NotImplementedException();
        }
        private void setUpScoreBoard(int score)
        {
            ///Debug.WriteLine(score);
            scorePosition newScore = new scorePosition();
            newScore.Score = score;
            highScore.Add(newScore);
            highScore.Sort();
            //ListView scores = new ListView();
            scoreBoard.ItemsSource = highScore;
            //save scores before leaving
            //Main.Children.Add(scores);
        }
    }
}