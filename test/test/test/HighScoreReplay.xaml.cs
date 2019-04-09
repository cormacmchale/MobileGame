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
        //will contain al prevoius scores, will work like a score board
        List<scorePosition> highScore = new List<scorePosition>();
        //takes in score from main page after collision with game objects
		public HighScoreReplay (int Score)
		{
			InitializeComponent();
            readScoresFromFile();
            setUpScoreBoard(Score);
		}
        private void readScoresFromFile()
        {
            //read in saved scores or default scores
            highScore = fileReaderHighScores.readInHighScoreList();
        }
        private void setUpScoreBoard(int score)
        {
            //get the score from the main page
            scorePosition newScore = new scorePosition();
            //save it into a score object
            newScore.Score = score;
            //add it to the list
            highScore.Add(newScore);
            //sort the list for user to see highest scores
            highScore.Sort();
            //only display top 5 score
            //after new score is added and list sorted, remove last element 
            highScore.RemoveAt(highScore.Count - 1);
            //save scores to local storage
            fileReaderHighScores.SaveHighScoreList(highScore);
            //ListView scores = new ListView();
            scoreBoard.ItemsSource = highScore;
        }
    }
}