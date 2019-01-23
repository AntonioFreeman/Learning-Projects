using Examenator.Classes;
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
        private int amountTask;
        public ResultViewModel(Result result)
        {
            amountTask = result.AmountTask;
            TimeExecute = result.TimeExecute;
            CorrectAnswers = result.CorrectAnswers;
            UncorrectAnswers = result.UncorrectAnswers;
            CalculationEstimate();
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
                CalculationEstimate();
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
                CalculationEstimate();
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
            double procent = 0;
            if ((correctAnswers > 0)||(uncorrectAnswers >0)) procent = correctAnswers / amountTask ;
            if (procent < 0.55) Estimate = 2;
            else if (procent < 0.7) Estimate = 3;
            else if (procent < 0.85) Estimate = 4;
            else Estimate = 5;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}
