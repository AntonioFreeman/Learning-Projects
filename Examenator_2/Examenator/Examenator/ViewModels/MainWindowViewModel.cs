using Examenator.AbstractClasses;
using Examenator.Classes;
using Examenator.Views;
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
        public EditExamenWindow editWindow;

        private string title;
        private int amountTasks;
        private TimeSpan timeTest;
           
        private RelayCommand startExamenCommand;
        public RelayCommand StartExamenCommand
        {
            get
            {
                return startExamenCommand ?? (startExamenCommand = new RelayCommand(obj =>
                {
                    new ExamenWindow(SelectedExamen);
                }, (obj) => SelectedExamen != null));
            }
        }

        private RelayCommand addExamenCommand;

        public RelayCommand AddExamenCommand
        {
            get
            {
                return addExamenCommand ?? (addExamenCommand = new RelayCommand(obj =>
                {
                    Examen examen = new Examen();
                    examen.Subject = "Новый экзамен";
                    examen.Tasks = new ObservableCollection<BaseTask>();
                    Examens.Add(examen);                  
                    SelectedExamen = examen;
                    editWindow = new EditExamenWindow(examen);
                    editWindow.Show();
                }));
            }
        }

        private RelayCommand editExamenCommand;

        public RelayCommand EditExamenCommand
        {
            get
            {
                return editExamenCommand ?? (editExamenCommand = new RelayCommand(obj =>
                {
                    editWindow = new EditExamenWindow(SelectedExamen);
                    editWindow.Show();
                }, (obj) => SelectedExamen != null));
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

        public MainWindowViewModel()
        {
            Examens = new ObservableCollection<Examen>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
