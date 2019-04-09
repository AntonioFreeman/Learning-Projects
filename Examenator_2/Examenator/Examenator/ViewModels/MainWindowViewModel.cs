using Examenator.AbstractClasses;
using Examenator.Classes;
using Examenator.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Examenator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {      
        private Loader loader;
        private DataSet ds;
        private DataRow currentExam;
        private Result Result;
        public EditExamWindow EditWindow;
        public PasswordWindow PasswordWindow;
        public EnterNameWindow EnterWindow;

        private DataTable examsTable;
        public DataTable ExamsTable
        {
            get { return examsTable; }
            set
            {
                examsTable = value;
                OnPropertyChanged("ExamsTable");
            }
        }

        private int amountTasks;
        public int AmountTasks
        {
            get { return amountTasks; }
            set
            {
                amountTasks = value;
            }
        }

        private int timeExam;
        public int TimeExam
        {
            get { return timeExam; }
            set
            {
                timeExam = value;
            }
        }

        private int selectedExam;
        public int SelectedExam
        {
            get { return selectedExam; }
            set
            {
                selectedExam = value;
                if(selectedExam < 0)
                {
                    AmountTasks = 0;
                    TimeExam = 0;
                }
                else
                {
                    AmountTasks = (int)ExamsTable.Rows[selectedExam]["AmountTasks"];
                    TimeExam = (int)ExamsTable.Rows[selectedExam]["TimeExam"];
                }
                OnPropertyChanged("SelectedExam");
            }
        }

        public MainWindowViewModel()
        {
            loader = new Loader();
            ds = loader.Load(loader.AdapterExams);
            ExamsTable = ds.Tables[0];
            selectedExam = -1;
        }

        private bool termAmountTask()
        {
            return AmountTasks > 0 ;
        }

        private bool termTimeExam()
        {
            return TimeExam > 0 && AmountTasks <= 720;
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

            var tasksTable = loader.Load(loader.AdapterTasks).Tables[0];

            var storageTasks = tasksTable.Select(string.Format("Id_Exam = {0}", newExam.Id));
            foreach (var t in storageTasks)
            {
                var newTask = new TextTask();
                newTask.Title = (string)t["Title"];
                newTask.Question = (string)t["Question"];
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

        private RelayCommand startExamCommand;
        public RelayCommand StartExamCommand
        {
            get
            {
                return startExamCommand ?? (startExamCommand = new RelayCommand(obj =>
                {
                    currentExam = examsTable.Rows[SelectedExam];
                    var exam = CreateExam((int)currentExam["Id"]);
                    Result = new Result(exam);
                    EnterWindow = new EnterNameWindow(Result, exam);
                    EnterWindow.Show();
                }, (obj) => (SelectedExam >= 0 && termAmountTask() && termTimeExam())));
            }
        }

        private RelayCommand addExamCommand;
        public RelayCommand AddExamCommand
        {
            get
            {
                return addExamCommand ?? (addExamCommand = new RelayCommand(obj =>
                {
                    EditWindow = new EditExamWindow(ExamsTable, loader);
                    EditWindow.ShowDialog();
                }));
            }
        }

        private RelayCommand editExamCommand;
        public RelayCommand EditExamCommand
        {
            get
            {
                return editExamCommand ?? (editExamCommand = new RelayCommand(obj =>
                {
                    currentExam = examsTable.Rows[SelectedExam];
                if (currentExam["Password"] == DBNull.Value) 
                    {
                        EditWindow = new EditExamWindow((int)currentExam["Id"], ExamsTable, loader);
                        EditWindow.ShowDialog();
                    }
                    else
                    {
                        PasswordWindow = new PasswordWindow(currentExam, ExamsTable, loader);
                        PasswordWindow.ShowDialog();
                    }                  
                }, (obj) => SelectedExam >= 0));
            }
        }

        private RelayCommand removeExamCommand;
        public RelayCommand RemoveExamCommand
        {
            get
            {
                return removeExamCommand ?? (removeExamCommand = new RelayCommand(obj =>
                {
                    examsTable.Rows[SelectedExam].Delete();
                    loader.Save(loader.AdapterExams, examsTable);
                }, (obj) => SelectedExam >= 0));
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
