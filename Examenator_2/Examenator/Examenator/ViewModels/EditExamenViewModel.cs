using Examenator.AbstractClasses;
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
        
        public Examen CurrentExamen;
        public void UpdateCurrentTask(TextTask task)
        {
            var currentTask = new TextTask();
            currentTask.Question = task.Question ?? "";
            currentTask.Title = task.Title ?? "";
            for (int i = 0; i < 4; i++) 
            {
                currentTask.Answers.ElementAt(i).Check = task.Answers.ElementAt(i).Check;
                currentTask.Answers.ElementAt(i).Correct = task.Answers.ElementAt(i).Correct;
                currentTask.Answers.ElementAt(i).ValueAnswer = task.Answers.ElementAt(i).ValueAnswer;              
            };
            CurrentTask = currentTask;
            OnPropertyChanged("CurrentTask");
            OnPropertyChanged("Answer_1");
            OnPropertyChanged("Answer_2");
            OnPropertyChanged("Answer_3");
            OnPropertyChanged("Answer_4");
        }

        public EditExamenViewModel(Examen examen)
        {
            CurrentExamen = examen;
            Subject = examen.Subject;
            CurrentTask = new TextTask();
        }

        public string Subject
        {
            get { return CurrentExamen.Subject; }
            set
            {
                CurrentExamen.Subject = value;
                OnPropertyChanged("Subject");
            }
        }

        public ObservableCollection<BaseTask> Tasks
        {
            get { return CurrentExamen.Tasks; }
            set
            {
                CurrentExamen.Tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        //public string Question
        //{
        //    get { return CurrentTask.Question; }
        //    set
        //    {
        //        CurrentTask.Question = value;
        //        OnPropertyChanged("Question");
        //    }
        //}

        public string Answer_1
        {
            get { return CurrentTask.Answers.ElementAt(0).ValueAnswer; }
            set
            {
                CurrentTask.Answers.ElementAt(0).ValueAnswer = value;
                OnPropertyChanged("Answer_1");
            }
        }

        public string Answer_2
        {
            get { return CurrentTask.Answers.ElementAt(1).ValueAnswer; }
            set
            {
                CurrentTask.Answers.ElementAt(1).ValueAnswer = value;
                OnPropertyChanged("Answer_2");
            }
        }

        public string Answer_3
        {
            get { return CurrentTask.Answers.ElementAt(2).ValueAnswer; }
            set
            {
                CurrentTask.Answers.ElementAt(2).ValueAnswer = value;
                OnPropertyChanged("Answer_3");
            }
        }

        public string Answer_4
        {
            get { return CurrentTask.Answers.ElementAt(3).ValueAnswer; }
            set
            {
                CurrentTask.Answers.ElementAt(3).ValueAnswer = value;
                OnPropertyChanged("Answer_4");
            }
        }

        private TextTask currentTask;
        public TextTask CurrentTask
        {
            get { return currentTask; }
            set
            {
                currentTask = value;
                OnPropertyChanged("CurrentTask");
            }
        }

        private TextTask selectedTask;
        public TextTask SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                if (selectedTask != null) UpdateCurrentTask(selectedTask);
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
                    TextTask task = new TextTask();
                    Tasks.Add(task); 
                    task.Title = "Задание № " + Convert.ToString(CurrentExamen.Tasks.IndexOf(task) + 1);                                      
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
                    {
                        int index = CurrentExamen.Tasks.IndexOf(SelectedTask);
                        CurrentExamen.Tasks.Remove(SelectedTask);
                        CurrentExamen.Tasks.Insert(index, CurrentTask);
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
