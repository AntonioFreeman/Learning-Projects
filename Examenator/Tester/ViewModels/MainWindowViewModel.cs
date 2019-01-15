using Examenator.Clases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Examen selectedExamen;

        public ObservableCollection<Examen> Examens { get; set; }

        private string title;
        private int amountTasks;
        private TimeSpan timeTest;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("TitleExamen");
            }
        }

        public int AmountQuestions
        {
            get { return amountTasks; }
            set
            {
                amountTasks = value;
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public Examen CreateExamen()
        {
            return new Examen();
        }

        public void DeliteExamen()
        {

        }

        public void EditExamen()
        {
            
        }

    }
}


