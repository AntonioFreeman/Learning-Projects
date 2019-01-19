using Examenator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.AbstractClasses
{
    public abstract class BaseAnswer : IAnswer
    {
        private bool correct;
        public bool Correct
        {
            get { return correct; }
            set
            {
                correct = value;
                OnPropertyChanged("Correct");
            }
        }

        private bool check;
        public bool Check
        {
            get { return check; }
            set
            {
                check = value;
                OnPropertyChanged("Check");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
