using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator
{
    public class ExamenViewModel : INotifyPropertyChanged
    {
        private Examen selectedTest;

        public ObservableCollection<Examen> Tests { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string title;
        private int amountQuestions;
        private TimeSpan timeTest;
        private List<Question> questions;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("TitleTest");
            }
        }

        public int AmountQuestions
        {
            get { return amountQuestions; }
            set
            {
                amountQuestions = value;
                OnPropertyChanged("AmountQuestions");
            }
        }

        public TimeSpan TimeTest
        {
            get { return timeTest; }
            set
            {
                timeTest = value;
                OnPropertyChanged("TimeTest");
            }
        }

        public List<Question> Questions
        {
            get { return questions; }
            set
            {
                questions = value;
                OnPropertyChanged("Questions");
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
}
