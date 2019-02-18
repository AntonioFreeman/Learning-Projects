using Examenator.AbstractClasses;
using Examenator.Classes;
using Examenator.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Examenator.ViewModels
{
    public class EditExamenViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int idCurrentExamen;
        private bool editFlag = false;
        private SqlDataAdapter adapterTasks;
        private SqlDataAdapter adapterExamens;
        private DataRow[] storageTasks;       
        private Loader loader;
        private DataTable examensTable { get; set; }
        private DataTable tasksTable { get; set; }
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

        public EditExamenViewModel(DataTable et, Loader ldr)
        {
            loader = ldr;
            adapterTasks = ldr.AdapterTasks;
            adapterExamens = ldr.AdapterExamens;
            examensTable = et;
            //tasksTable = ldr.Load(adapterTasks).Tables[0];
            CurrentExamen = new Examen();          
            CurrentTask = new TextTask();
        }

        public EditExamenViewModel(DataTable et, int idExamen, Loader ldr)
        {
            loader = ldr;
            adapterTasks = ldr.AdapterTasks;
            editFlag = true;
            idCurrentExamen = idExamen;
            examensTable = et;
            SqlCommand commandLoadTasks = new SqlCommand(string.Format("SELECT * FROM Tasks WHERE Id_Examen = {0}", idExamen), loader.Connection);
            adapterTasks.SelectCommand = commandLoadTasks; 
            tasksTable = ldr.Load(adapterTasks).Tables[0];
            CurrentExamen = CreateExamen(idCurrentExamen);
            CurrentTask = new TextTask();
        }

        private Examen CreateExamen(int idExamen)
        {
            DataRow examen = examensTable.Select(string.Format("Id = {0}", idExamen))[0];
            var newExamen = new Examen();
            newExamen.Subject = (examen["Subj"] != DBNull.Value) ? (string)examen["Subj"] : null;
            newExamen.Password = (examen["Pswrd"] != DBNull.Value) ? (string)examen["Pswrd"] : null;
            newExamen.Procent_3 = (int)examen["Procent_3"];
            newExamen.Procent_4 = (int)examen["Procent_4"];
            newExamen.Procent_5 = (int)examen["Procent_5"];
            newExamen.AmountTask = (int)examen["AmountTasks"];
            newExamen.TimeExamen = (int)examen["TimeExamen"];
            newExamen.Id = (int)examen["Id"];

            storageTasks = tasksTable.Select(string.Format("Id_Examen = {0}", newExamen.Id));
            foreach( var t in storageTasks)
            {
                var newTask = new TextTask();
                newTask.Title = (string)t["Title"];
                newTask.Question = (string)t["Question"];
                newTask.Id = (int)t["Id"];
                newTask.Id_Examen = (int)t["Id_Examen"];
                newTask.Answers.Clear();
                newTask.Answers.Add(new TextAnswer() { ValueAnswer = (string)t["Answer_1"], Correct = (bool)t["Correct_Ans_1"] });
                newTask.Answers.Add(new TextAnswer() { ValueAnswer = (string)t["Answer_2"], Correct = (bool)t["Correct_Ans_2"] });
                newTask.Answers.Add(new TextAnswer() { ValueAnswer = (string)t["Answer_3"], Correct = (bool)t["Correct_Ans_3"] });
                newTask.Answers.Add(new TextAnswer() { ValueAnswer = (string)t["Answer_4"], Correct = (bool)t["Correct_Ans_4"] });
                newExamen.Tasks.Add(newTask);
            }
            return newExamen;
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
                        foreach (var sT in storageTasks)
                        {
                            int index = tasksTable.Rows.IndexOf(sT);
                            tasksTable.Rows[index].Delete();
                        }
                        AddTasks();                      
                        SelectedTask = null;
                        CurrentExamen = CreateExamen(idCurrentExamen);
                    }
                    else
                    {
                        AddExamen();
                        SqlCommand commandLoadTasks = new SqlCommand(string.Format("SELECT * FROM Tasks WHERE Id_Examen = {0}", idCurrentExamen), loader.Connection);
                        adapterTasks.SelectCommand = commandLoadTasks;
                        tasksTable = loader.Load(adapterTasks).Tables[0];
                        AddTasks();
                        editFlag = true;
                        CurrentExamen = CreateExamen(idCurrentExamen);
                    }
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
                    SettingWindow.ShowDialog();
                }));
            }
        }

        private void AddExamen()
        {
            DataRow newRow = examensTable.NewRow();
            newRow["Subj"] = CurrentExamen.Subject;
            newRow["Pswrd"] = CurrentExamen.Password;
            newRow["Procent_3"] = CurrentExamen.Procent_3;
            newRow["Procent_4"] = CurrentExamen.Procent_4;
            newRow["Procent_5"] = CurrentExamen.Procent_5;
            newRow["AmountTasks"] = CurrentExamen.AmountTask;
            newRow["TimeExamen"] = CurrentExamen.TimeExamen;
            examensTable.Rows.Add(newRow);
            loader.Save(adapterExamens, examensTable);
            idCurrentExamen = (int)newRow["Id"];
        }

        private void AddTasks()
        {
            foreach (var t in CurrentExamen.Tasks)
            {
                DataRow row = tasksTable.NewRow();
                row["Title"] = t.Title;
                row["Question"] = t.Question;
                row["Answer_1"] = t.Answers[0].ValueAnswer;
                row["Correct_Ans_1"] = t.Answers[0].Correct;
                row["Answer_2"] = t.Answers[1].ValueAnswer;
                row["Correct_Ans_2"] = t.Answers[1].Correct;
                row["Answer_3"] = t.Answers[2].ValueAnswer;
                row["Correct_Ans_3"] = t.Answers[2].Correct;
                row["Answer_4"] = t.Answers[3].ValueAnswer;
                row["Correct_Ans_4"] = t.Answers[3].Correct;
                row["Id_Examen"] = idCurrentExamen;
                tasksTable.Rows.Add(row);                
            }
            loader.Save(adapterTasks, tasksTable);
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
