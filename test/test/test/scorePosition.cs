using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace test
{
    class scorePosition : INotifyPropertyChanged, IComparable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _score;
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
            //Debug.WriteLine("Change");
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(object obj)
        {
            return Score.CompareTo(((scorePosition)obj).Score)*-1;
        }
    }

}
