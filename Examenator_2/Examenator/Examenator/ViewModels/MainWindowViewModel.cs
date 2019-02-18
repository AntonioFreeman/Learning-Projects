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
        private DataRow currentExamen;
        private Result Result;
        public EditExamenWindow EditWindow;
        public PasswordWindow PasswordWindow;
        public EnterNameWindow EnterWindow;

        private DataTable examensTable;
        public DataTable ExamensTable
        {
            get { return examensTable; }
            set
            {
                examensTable = value;
                OnPropertyChanged("ExamensTable");
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

        private int timeExamen;
        public int TimeExamen
        {
            get { return timeExamen; }
            set
            {
                timeExamen = value;
            }
        }

        private int selectedExamen;
        public int SelectedExamen
        {
            get { return selectedExamen; }
            set
            {
                selectedExamen = value;
                if(selectedExamen < 0)
                {
                    AmountTasks = 0;
                    TimeExamen = 0;
                }
                else
                {
                    AmountTasks = (int)ExamensTable.Rows[selectedExamen]["AmountTasks"];
                    TimeExamen = (int)ExamensTable.Rows[selectedExamen]["TimeExamen"];
                }
                OnPropertyChanged("SelectedExamen");
            }
        }

        public MainWindowViewModel()
        {
            loader = new Loader();
            ds = loader.Load(loader.AdapterExamens);
            ExamensTable = ds.Tables[0];
            selectedExamen = -1;
        }

        private bool termAmountTask()
        {
            return AmountTasks > 0 ;
        }

        private bool termTimeExamen()
        {
            return TimeExamen > 0 && AmountTasks <= 720;
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

            var tasksTable = loader.Load(loader.AdapterTasks).Tables[0];

            var storageTasks = tasksTable.Select(string.Format("Id_Examen = {0}", newExamen.Id));
            foreach (var t in storageTasks)
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

        private RelayCommand startExamenCommand;
        public RelayCommand StartExamenCommand
        {
            get
            {
                return startExamenCommand ?? (startExamenCommand = new RelayCommand(obj =>
                {
                    currentExamen = examensTable.Rows[SelectedExamen];
                    var examen = CreateExamen((int)currentExamen["Id"]);
                    Result = new Result(examen);
                    EnterWindow = new EnterNameWindow(Result, examen);
                    EnterWindow.Show();
                }, (obj) => (SelectedExamen >= 0 && termAmountTask() && termTimeExamen())));
            }
        }

        private RelayCommand addExamenCommand;
        public RelayCommand AddExamenCommand
        {
            get
            {
                return addExamenCommand ?? (addExamenCommand = new RelayCommand(obj =>
                {
                    EditWindow = new EditExamenWindow(ExamensTable, loader);
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
                    currentExamen = examensTable.Rows[SelectedExamen];
                if (currentExamen["Pswrd"] == DBNull.Value) 
                    {
                        EditWindow = new EditExamenWindow((int)currentExamen["Id"], ExamensTable, loader);
                        EditWindow.ShowDialog();
                    }
                    else
                    {
                        PasswordWindow = new PasswordWindow(currentExamen, ExamensTable, loader);
                        PasswordWindow.ShowDialog();
                    }                  
                }, (obj) => SelectedExamen >= 0));
            }
        }

        private RelayCommand removeExamenCommand;
        public RelayCommand RemoveExamenCommand
        {
            get
            {
                return removeExamenCommand ?? (removeExamenCommand = new RelayCommand(obj =>
                {
                    examensTable.Rows[SelectedExamen].Delete();
                    loader.Save(loader.AdapterExamens, examensTable);
                }, (obj) => SelectedExamen >= 0));
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
