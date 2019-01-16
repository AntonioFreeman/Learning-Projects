using Examenator.AbstractClases;
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
    public class EditExamenViewModel : INotifyPropertyChanged
    {
        public string Subject { get; set; }

        public ObservableCollection<BaseTask> Tasks { get; set; }

        public EditExamenViewModel(Examen examen)
        {
            Subject = examen.Subject;
            Tasks = examen.Tasks;
        }

        private BaseTask selectedTask;

        public BaseTask SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        private RelayCommand addTaskCommand;

        public RelayCommand AddTaskCommand
        {
            get
            {
                return addTaskCommand ?? (addTaskCommand = new RelayCommand(obj =>
                {
                    BaseTask task = new TextTask();
                    Tasks.Add(task);
                    task.Title = "Задание № " + Convert.ToString(Tasks.IndexOf(task) + 1);
                    SelectedTask = task;
                }));
            }
        }
        private RelayCommand saveEditTaskCommand;
        public RelayCommand SaveEditTaskCommand
        {
            get
            {
                return saveEditTaskCommand ?? (saveEditTaskCommand = new RelayCommand(obj =>
                {
                    TextTask task = obj as TextTask;
                    if (task != null)
                    {
                        var index = Tasks.IndexOf(SelectedTask);
                        Tasks.RemoveAt(index);
                        Tasks.Insert(index, task);
                    }
                }, (obj) => SelectedTask != null));
            }
        }

        private RelayCommand removeTaskCommand;

        public RelayCommand RemoveTaskCommand
        {
            get
            {
                return removeTaskCommand ?? (removeTaskCommand = new RelayCommand(obj =>
                {
                    TextTask task = obj as TextTask;
                    if (task != null)
                    {
                        Tasks.Remove(task);
                    }
                }, (obj) => Tasks.Count > 0));
            }
        }

        private RelayCommand saveEditExamenCommand;
        public RelayCommand SaveEditExamenCommand
        {
            get
            {
                return saveEditExamenCommand ?? (saveEditExamenCommand = new RelayCommand(
                    
                    ));
            }
        }

        public EditExamenViewModel()
        {
            Tasks = new ObservableCollection<BaseTask>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }


    }
}
