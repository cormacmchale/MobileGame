﻿using System;
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

        private void setUpScoreBoard(int score)
        {
            Debug.WriteLine(score);
            //ListView scores = new ListView();
            scoreBoard.ItemsSource = highScore;
            //Main.Children.Add(scores);
        }

        private void readScoresFromFile()
        {
            Debug.Write("In Method \n");
            highScore = fileReaderHighScores.readInHighScoreList();
            //Debug.Write(highScore[0].Score);
            //throw new NotImplementedException();
        }
    }
}