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
        private Loader loader;
        public Result Result;
        public ObservableCollection<Examen> Examens { get; set; }
        public EditExamenWindow EditWindow;
        public PasswordWindow PasswordWindow;
        public EnterNameWindow EnterWindow;

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

        private Examen selectedExamen;
        public Examen SelectedExamen
        {
            get { return selectedExamen; }
            set
            {
                selectedExamen = value;
                if(selectedExamen != null)
                {
                    TimeExamen = selectedExamen.TimeExamen;
                    AmountTasks = selectedExamen.AmountTask;
                }                
                OnPropertyChanged("SelectedExamen");
            }
        }               

        public MainWindowViewModel()
        {
            loader = new Loader();
            try 
                { Examens = loader.LoadDeserialisation(); }
            catch
                { Examens = new ObservableCollection<Examen>(); }           
        }
                
        private bool termAmountTask()
        {
            return AmountTasks > 0 && AmountTasks <= SelectedExamen.Tasks.Count;
        }

        private bool termTimeExamen()
        {
            return TimeExamen > 0 && AmountTasks <= 720;
        }

        private RelayCommand startExamenCommand;
        public RelayCommand StartExamenCommand
        {
            get
            {
                return startExamenCommand ?? (startExamenCommand = new RelayCommand(obj =>
                {
                    Result = new Result(SelectedExamen);
                    EnterWindow = new EnterNameWindow(Result, SelectedExamen);
                    EnterWindow.Show();
                }, (obj) => (SelectedExamen != null && termAmountTask() && termTimeExamen())));
            }
        }

        private RelayCommand addExamenCommand;
        public RelayCommand AddExamenCommand
        {
            get
            {
                return addExamenCommand ?? (addExamenCommand = new RelayCommand(obj =>
                {
                    EditWindow = new EditExamenWindow(Examens);
                    EditWindow.ShowDialog();
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
                    PasswordWindow = new PasswordWindow(SelectedExamen, Examens);
                    PasswordWindow.ShowDialog();
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
                        loader.SaveSerialisation(Examens);
                    }
                }, (obj) => Examens.Count > 0));
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
