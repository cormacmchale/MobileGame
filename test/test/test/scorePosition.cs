using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace test
{
    //this class will be used to track score and display it to the user
    //it will also be sent to a list to be sorted so it implements the Icomparable interface
    //this sorted list will be showed to the user as a high score table
    class scorePosition : INotifyPropertyChanged, IComparable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _score;
        //implement Property changed so UI is updated correctly for user
        public int Score
        {
                    get
                    {
                        return _score;
                    }
                    set
                    {
                        if (_score == value) return; // no change
                        _score = value;
                        OnPropertyChanged(nameof(Score));
                    }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
        //allow the list to be sorted
        public int CompareTo(object obj)
        {
            return Score.CompareTo(((scorePosition)obj).Score)*-1;
        }
    }
}
