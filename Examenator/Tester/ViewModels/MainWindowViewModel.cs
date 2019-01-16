using Examenator.Clases;
using Examenator.Classes;
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
        public ObservableCollection<Examen> Examens { get; set; }

        private string title;
        private int amountTasks;
        private TimeSpan timeTest;

        private RelayCommand addExamenCommand;

        public RelayCommand AddExamenCommand
        {
            get
            {
                return addExamenCommand ?? (addExamenCommand = new RelayCommand(obj =>
                 {
                     Examen examen = new Examen();
                     Examens.Add(examen);
                     SelectedExamen = examen;
                     new EditExamenViewModel(examen);
                 }));
            }
        }

        private RelayCommand removeExamenCommand;

        public RelayCommand RemoveExamenCommand
        {
            get
            {
                return removeExamenCommand ?? (removeExamenCommand = new RelayCommand(obj =>
                 {
                     Examen examen = obj as Examen;
                     if (examen != null)
                     {
                         Examens.Remove(examen);
                     }
                 }, (obj) => Examens.Count > 0));
            }
        }


        private Examen selectedExamen;

        public Examen SelectedExamen
        {
            get { return selectedExamen; }
            set
            {
                selectedExamen = value;
                OnPropertyChanged("SelectedExamen");
            }
        }

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

        public MainWindowViewModel ()
        {
            Examens = new ObservableCollection<Examen>();
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


