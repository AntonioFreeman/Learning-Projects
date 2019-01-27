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
using System.Windows;

namespace Examenator.ViewModels
{
    public class EditExamenViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private bool editFlag = false;
        private int indexExamen;
        private Loader loader;
        public ObservableCollection<Examen> Examens { get; set; }
        public Examen CurrentExamen;
        public SettingWindow SettingWindow;

        public void UpdateCurrentTask(TextTask task)
        {
            var currentTask = (TextTask)task.Clone();
            CurrentTask = currentTask;
            OnPropertyChanged("CurrentTask");
            OnPropertyChanged("Answer_1");
            OnPropertyChanged("Answer_2");
            OnPropertyChanged("Answer_3");
            OnPropertyChanged("Answer_4");
            OnPropertyChanged("CheckBox_1");
            OnPropertyChanged("CheckBox_2");
            OnPropertyChanged("CheckBox_3");
            OnPropertyChanged("CheckBox_4");
        }

        public EditExamenViewModel(ObservableCollection<Examen> examens)
        {
            loader = new Loader();
            indexExamen = examens.Count;
            Examens = examens;
            CurrentExamen = new Examen();          
            CurrentTask = new TextTask();
        }

        public EditExamenViewModel(Examen examen, ObservableCollection<Examen> examens)
        {
            editFlag = true;
            indexExamen = examens.IndexOf(examen);
            loader = new Loader();
            Examens = examens;
            CurrentExamen = (Examen)examen.Clone();
            CurrentTask = new TextTask();
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

        public int AmountTasks
        {
            get { return CurrentExamen.AmountTask; }
            set
            {
                    CurrentExamen.AmountTask = value;
                    OnPropertyChanged("AmountTasks");           
            }
        }

        public int TimeExamen
        {
            get { return CurrentExamen.TimeExamen; }
            set
            {
                    CurrentExamen.TimeExamen = value;
                    OnPropertyChanged("TimeExamen");
            }
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

        public bool? CheckBox_1
        {
            get { return CurrentTask.Answers.ElementAt(0).Correct; }
            set
            {
                CurrentTask.Answers.ElementAt(0).Correct = (bool)value;
                OnPropertyChanged("CheckBox_1");
            }
        }

        public bool? CheckBox_2
        {
            get { return CurrentTask.Answers.ElementAt(1).Correct; }
            set
            {
                CurrentTask.Answers.ElementAt(1).Correct = (bool)value;
                OnPropertyChanged("CheckBox_2");
            }
        }

        public bool? CheckBox_3
        {
            get { return CurrentTask.Answers.ElementAt(2).Correct; }
            set
            {
                CurrentTask.Answers.ElementAt(2).Correct = (bool)value;
                OnPropertyChanged("CheckBox_3");
            }
        }

        public bool? CheckBox_4
        {
            get { return CurrentTask.Answers.ElementAt(3).Correct; }
            set
            {
                CurrentTask.Answers.ElementAt(3).Correct = (bool)value;
                OnPropertyChanged("CheckBox_4");
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
                        int index = Tasks.IndexOf(task);
                        Tasks.Remove(task);
                        for (int i = index; i < Tasks.Count; i++)
                        {
                            Tasks[i].Title = "Задание № " + (i + 1).ToString();
                        }
                        if (index < Tasks.Count) SelectedTask = (TextTask)Tasks[index];
                    }
                }, (obj) => Tasks.Count > 0));
            }
        }

        private RelayCommand saveExamensCommand;
        public RelayCommand SaveExamensCommand
        {
            get
            {
                return saveExamensCommand ?? (saveExamensCommand = new RelayCommand(obj =>
                {
                    if (editFlag)
                    {
                        Examens.Insert(indexExamen, CurrentExamen);
                        Examens.RemoveAt(indexExamen + 1);
                    }
                    else
                    {
                        Examens.Add(CurrentExamen);
                        editFlag = true;
                    }
                    loader.SaveSerialisation(Examens);
                }));
            }
        }

        private RelayCommand settingCommand;
        public RelayCommand SettingCommand
        {
            get
            {
                return settingCommand ?? (settingCommand = new RelayCommand(obj =>
                {
                    var SettingWindow = new SettingWindow(CurrentExamen);
                    SettingWindow.Show();
                }));
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "AmountTasks":
                        if ((AmountTasks < 1) || (AmountTasks > Tasks.Count))
                        {
                            error = "Введите количество вопросов не менее 1 и не более общего количества вопросов";
                        }
                        break;
                    case "TimeExamen":
                        if ((TimeExamen < 1) || (TimeExamen > 720))
                        {
                            error = "Введите время экзамена от 1 и до 720 минут";
                        }
                        break;
                }
                return error;
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
