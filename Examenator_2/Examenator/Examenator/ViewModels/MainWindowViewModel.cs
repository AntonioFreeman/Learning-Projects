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
        public EditExamenWindow EditWindow;
        public ExamenWindow ExamenWindow;

        private string title;
        
        
           
        private RelayCommand startExamenCommand;
        public RelayCommand StartExamenCommand
        {
            get
            {
                return startExamenCommand ?? (startExamenCommand = new RelayCommand(obj =>
                {
                    ExamenWindow = new ExamenWindow(SelectedExamen, AmountTasks, TimeExamen );
                    ExamenWindow.Show();
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
                    EditWindow = new EditExamenWindow(examen);
                    EditWindow.Show();
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
                    EditWindow = new EditExamenWindow(SelectedExamen);
                    EditWindow.Show();
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
                AmountTasks = selectedExamen.Tasks.Count;
                TimeExamen = 60;
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

        private int amountTasks;
        public int AmountTasks
        {
            get { return amountTasks; }
            set
            {
                amountTasks = value;
                OnPropertyChanged("AmountTasks");
            }
        }
        private int timeExamen;
        public int TimeExamen
        {
            get { return timeExamen; }
            set
            {
                timeExamen = value;
                OnPropertyChanged("TimeExamen");
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
