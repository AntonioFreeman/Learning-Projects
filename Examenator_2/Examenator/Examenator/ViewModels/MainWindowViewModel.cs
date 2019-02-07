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
        string connectionString;
        SqlDataAdapter adapterExamens;
        SqlDataAdapter adapterTasks;
        private DataTable tasksTable;
        private Loader loader;
        public DataSet Ds;
        
        public Result Result;
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

        //private int amountTasks;
        //public int AmountTasks
        //{
        //    get { return amountTasks; }
        //    set
        //    {
        //        amountTasks = value;
        //        OnPropertyChanged("AmountTasks");
        //    }
        //}

        //private int timeExamen;
        //public int TimeExamen
        //{
        //    get { return timeExamen; }
        //    set
        //    {
        //        timeExamen = value;
        //        OnPropertyChanged("TimeExamen");
        //    }
        //}

        private int selectedExamen = -1;
        public int SelectedExamen
        {
            get { return selectedExamen; }
            set
            {
                selectedExamen = value;               
                OnPropertyChanged("SelectedExamen");
            }
        }

        public MainWindowViewModel()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sql_1 = "SELECT * FROM Examens";
            string sql_2 = "SELECT * FROM Tasks";
            Ds = new DataSet();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand commandLoadExamens = new SqlCommand(sql_1, connection);
                SqlCommand commandLoadTasks = new SqlCommand(sql_2, connection);
                adapterExamens = new SqlDataAdapter(commandLoadExamens);
                adapterTasks = new SqlDataAdapter(commandLoadTasks);

                adapterExamens.InsertCommand = new SqlCommand("sp_InsertExamen", connection);
                adapterExamens.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@subject", SqlDbType.Text, 0, "Subject"));
                adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 50, "Password"));
                adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@procent_3", SqlDbType.Int, 0, "Procent_3"));
                adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@procent_4", SqlDbType.Int, 0, "Procent_4"));
                adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@procent_5", SqlDbType.Int, 0, "Procent_5"));
                adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@amountTasks", SqlDbType.Int, 0, "AmountTasks"));
                adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@timeExamen", SqlDbType.Int, 0, "TimeExamen"));
                SqlParameter parameter = adapterExamens.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                adapterTasks.InsertCommand = new SqlCommand("sp_InsertTask", connection);
                adapterTasks.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@title", SqlDbType.Text, 0, "Title"));
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@question", SqlDbType.Text, 50, "Question"));
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@answer_1", SqlDbType.Text, 0, "Answer_1"));
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@correct_Ans_1", SqlDbType.Bit, 0, "Correct_Ans_1"));
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@answer_2", SqlDbType.Text, 0, "Answer_2"));
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@correct_Ans_2", SqlDbType.Bit, 0, "Correct_Ans_2"));
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@answer_3", SqlDbType.Text, 0, "Answer_3"));
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@correct_Ans_3", SqlDbType.Bit, 0, "Correct_Ans_3"));
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@answer_4", SqlDbType.Text, 0, "Answer_4"));
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@correct_Ans_4", SqlDbType.Bit, 0, "Correct_Ans_4"));
                adapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@id_Examen", SqlDbType.Int, 0, "Id_Examen"));
                SqlParameter parameterId = adapterExamens.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 0, "Id");
                parameterId.Direction = ParameterDirection.Output;

                connection.Open();
                adapterExamens.Fill(Ds);
                Ds.Tables.Add();
                adapterTasks.Fill(Ds.Tables[1]);
                examensTable = Ds.Tables[0];

                tasksTable = Ds.Tables[1];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            //loader = new Loader();
            //try 
            //    { Examens = loader.LoadDeserialisation(); }
            //catch
            //    { Examens = new ObservableCollection<Examen>(); }           
        }

        private void UpdateDB()
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapterExamens);
            adapterExamens.Update(examensTable);        
        }
                
        //private bool termAmountTask()
        //{
        //    return AmountTasks > 0 && AmountTasks <= SelectedExamen.Tasks.Count;
        //}

        //private bool termTimeExamen()
        //{
        //    return TimeExamen > 0 && AmountTasks <= 720;
        //}

        //private RelayCommand startExamenCommand;
        //public RelayCommand StartExamenCommand
        //{
        //    get
        //    {
        //        return startExamenCommand ?? (startExamenCommand = new RelayCommand(obj =>
        //        {
        //            Result = new Result(SelectedExamen);
        //            EnterWindow = new EnterNameWindow(Result, SelectedExamen);
        //            EnterWindow.Show();
        //        }, (obj) => (SelectedExamen != null && termAmountTask() && termTimeExamen())));
        //    }
        //}

        private RelayCommand addExamenCommand;
        public RelayCommand AddExamenCommand
        {
            get
            {
                return addExamenCommand ?? (addExamenCommand = new RelayCommand(obj =>
                {                 
                    EditWindow = new EditExamenWindow(Ds, adapterTasks, adapterExamens);
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
                    DataRow currentExamen = examensTable.Rows[SelectedExamen];
                    PasswordWindow = new PasswordWindow(currentExamen, Ds, adapterTasks); 
                    PasswordWindow.ShowDialog();
                }, (obj) => SelectedExamen >=0));
            }
        }

        private RelayCommand removeExamenCommand;
        public RelayCommand RemoveExamenCommand
        {
            get
            {
                return removeExamenCommand ?? (removeExamenCommand = new RelayCommand(obj =>
                {
                    examensTable.Rows.RemoveAt(SelectedExamen);

                    //Examen examen = obj as Examen;
                    //if (examen != null)
                    //{                        
                    //    Examens.Remove(examen);
                    //    loader.SaveSerialisation(Examens);
                    //}
                }/*, (obj) => Examens.Count > 0*/));
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
