using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace test
{
    class scorePosition : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _score;
        public string Score
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
    }
}
