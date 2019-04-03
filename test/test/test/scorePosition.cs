using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace test
{
    class scorePosition : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _score;
        public int Score {
                    get
                    {
                        return _score;
                    }
                    set
                    {
                        if (_score == value) return; // no change
                        _score = value;
                        // notify the system of the change
                        // there is a change, whatever is databound will update 
                        // if PC == null, do nothing, else Invoke the
                        // PC event handler with the two arguments
                        //PropertyChanged?.Invoke(this,
                        //    new PropertyChangedEventArgs(nameof(Size)));
                        OnPropertyChanged(nameof(Score));
                    }
               }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
