using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.ViewModels
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        public ResultViewModel()
        {

        }

        private TimeSpan timeExecute;
        public TimeSpan TimeExecute
        {
            get { return timeExecute; }
            set
            {
                timeExecute = value;
                OnPropertyChanged("TimeExecute");
            }
        }

        private int correctAnswers;
        public int CorrectAnswers
        {
            get { return correctAnswers; }
            set
            {
                correctAnswers = value;
                OnPropertyChanged("CorrectAnswers");
            }
        }

        private int uncorrectAnswers;
        public int UncorrectAnswers
        {
            get { return uncorrectAnswers; }
            set
            {
                uncorrectAnswers = value;
                OnPropertyChanged("UncorrectAnswers");
            }
        }

        private int estimate;
        public int Estimate
        {
            get { return estimate; }
            set
            {
                estimate = value;
                OnPropertyChanged("Estimate");
            }
        }

        public void CalculationEstimate()
        {
            double procent = 
            Estimate
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}
