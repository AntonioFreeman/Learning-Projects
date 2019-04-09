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
    public class EditExamViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int idCurrentExam;
        private bool editFlag = false;
        private SqlDataAdapter adapterTasks;
        private SqlDataAdapter adapterExams;
        //private DataRow[] storageTasks;       
        private Loader loader;
        private DataTable examsTable { get; set; }
        private DataTable tasksTable { get; set; }
        public Exam CurrentExam;
        public SettingWindow SettingWindow;

        public EditExamViewModel(DataTable examsTable, Loader loader)
        {
            this.loader = loader;
            adapterTasks = loader.AdapterTasks;
            adapterExams = loader.AdapterExams;
            this.examsTable = examsTable;
            CurrentExam = new Exam();          
            CurrentTask = new TextTask();
        }

        public EditExamViewModel(DataTable examsTable, int idExamen, Loader loader)
        {
            this.loader = loader;
            adapterTasks = loader.AdapterTasks;
            editFlag = true;
            idCurrentExam = idExamen;
            this.examsTable = examsTable;
            SqlCommand commandLoadTasks = new SqlCommand(string.Format("SELECT * FROM Tasks WHERE Id_Exam = {0}", idExamen), this.loader.Connection);
            adapterTasks.SelectCommand = commandLoadTasks; 
            tasksTable = loader.Load(adapterTasks).Tables[0];
            CurrentExam = CreateExam(idCurrentExam);
            CurrentTask = new TextTask();
        }

        public void UpdateCurrentTask(TextTask task)
        {
            //var currentTask = (TextTask)task.Clone();
            CurrentTask = task;
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

        private Exam CreateExam(int idExam)
        {
            DataRow exam = examsTable.Select(string.Format("Id = {0}", idExam))[0];
            var newExam = new Exam();
            newExam.Subject = (exam["Subject"] != DBNull.Value) ? (string)exam["Subject"] : null;
            newExam.Password = (exam["Password"] != DBNull.Value) ? (string)exam["Password"] : null;
            newExam.Procent_3 = (int)exam["Procent_3"];
            newExam.Procent_4 = (int)exam["Procent_4"];
            newExam.Procent_5 = (int)exam["Procent_5"];
            newExam.AmountTask = (int)exam["AmountTasks"];
            newExam.TimeExam = (int)exam["TimeExam"];
            newExam.Id = (int)exam["Id"];

            //storageTasks = tasksTable.Select(string.Format("Id_Exam = {0}", newExam.Id));
            foreach(DataRow t in tasksTable.Rows)
            {
                var newTask = new TextTask();
                newTask.Title = (t["Title"] != DBNull.Value) ? (string)t["Title"] : null;
                newTask.Question = (t["Question"] != DBNull.Value) ? (string)t["Question"] : null;
                newTask.Id = (int)t["Id"];
                newTask.Id_Exam = (int)t["Id_Exam"];
                newTask.Answers.Clear();
                newTask.Answers.Add(new TextAnswer() { ValueAnswer = (string)t["Answer_1"], Correct = (bool)t["Correct_Ans_1"] });
                newTask.Answers.Add(new TextAnswer() { ValueAnswer = (string)t["Answer_2"], Correct = (bool)t["Correct_Ans_2"] });
                newTask.Answers.Add(new TextAnswer() { ValueAnswer = (string)t["Answer_3"], Correct = (bool)t["Correct_Ans_3"] });
                newTask.Answers.Add(new TextAnswer() { ValueAnswer = (string)t["Answer_4"], Correct = (bool)t["Correct_Ans_4"] });
                newExam.Tasks.Add(newTask);
            }
            return newExam;
        }

        public ObservableCollection<BaseTask> Tasks
        {
            get { return CurrentExam.Tasks; }
            set
            {
                CurrentExam.Tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        public int AmountTasks
        {
            get { return CurrentExam.AmountTask; }
            set
            {
                    CurrentExam.AmountTask = value;
                    OnPropertyChanged("AmountTasks");           
            }
        }

        public int TimeExam
        {
            get { return CurrentExam.TimeExam; }
            set
            {
                    CurrentExam.TimeExam = value;
                    OnPropertyChanged("TimeExam");
            }
        }

        public string Subject
        {
            get { return CurrentExam.Subject; }
            set
            {
                CurrentExam.Subject = value;
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
                    task.Title = "Задание № " + Convert.ToString(CurrentExam.Tasks.IndexOf(task) + 1);                                      
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
                        int index = CurrentExam.Tasks.IndexOf(SelectedTask);
                        CurrentExam.Tasks.Remove(SelectedTask);
                        CurrentExam.Tasks.Insert(index, CurrentTask);
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

        private RelayCommand saveExamsCommand;
        public RelayCommand SaveExamsCommand
        {
            get
            {
                return saveExamsCommand ?? (saveExamsCommand = new RelayCommand(obj =>
                {
                    if (editFlag)
                    {
                        for (int i = 0; i < tasksTable.Rows.Count; i++)
                        {
                            tasksTable.Rows[i].Delete();
                        }                
                        AddTasks();

                        for (int i = 0; i < examsTable.Rows.Count; i++)
                        {
                            if ((int)examsTable.Rows[i]["Id"] == idCurrentExam)
                            {
                                examsTable.Rows[i]["Subject"] = CurrentExam.Subject;
                                examsTable.Rows[i]["Password"] = CurrentExam.Password;
                                examsTable.Rows[i]["Procent_3"] = CurrentExam.Procent_3;
                                examsTable.Rows[i]["Procent_4"] = CurrentExam.Procent_4;
                                examsTable.Rows[i]["Procent_5"] = CurrentExam.Procent_5;
                                examsTable.Rows[i]["AmountTasks"] = CurrentExam.AmountTask;
                                examsTable.Rows[i]["TimeExam"] = CurrentExam.TimeExam;
                            }
                        } 
                        SelectedTask = null;
                        CurrentExam = CreateExam(idCurrentExam);
                    }
                    else
                    {
                        AddExam();
                        SqlCommand commandLoadTasks = new SqlCommand(string.Format("SELECT * FROM Tasks WHERE Id_Exam = {0}", idCurrentExam), loader.Connection);
                        adapterTasks.SelectCommand = commandLoadTasks;
                        tasksTable = loader.Load(adapterTasks).Tables[0];
                        AddTasks();
                        editFlag = true;
                        CurrentExam = CreateExam(idCurrentExam);
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
                    var SettingWindow = new SettingWindow(CurrentExam);
                    SettingWindow.ShowDialog();
                }));
            }
        }

        private void AddExam()
        {
            DataRow newRow = examsTable.NewRow();
            newRow["Subject"] = CurrentExam.Subject;
            newRow["Password"] = CurrentExam.Password;
            newRow["Procent_3"] = CurrentExam.Procent_3;
            newRow["Procent_4"] = CurrentExam.Procent_4;
            newRow["Procent_5"] = CurrentExam.Procent_5;
            newRow["AmountTasks"] = CurrentExam.AmountTask;
            newRow["TimeExam"] = CurrentExam.TimeExam;
            examsTable.Rows.Add(newRow);
            loader.Save(adapterExams, examsTable);
            idCurrentExam = (int)newRow["Id"];
        }

        private void AddTasks()
        {
            foreach (var t in CurrentExam.Tasks)
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
                row["Id_Exam"] = idCurrentExam;
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
                    case "TimeExam":
                        if ((TimeExam < 1) || (TimeExam > 720))
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
