using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace test
{
    class scorePosition : INotifyPropertyChanged
    {
        private int score;
        public int Score { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
