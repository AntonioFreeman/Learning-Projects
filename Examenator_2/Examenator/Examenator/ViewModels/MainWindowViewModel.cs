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
        SqlDataAdapter adapter;
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

        private int selectedExamen;
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
            string sql = "SELECT * FROM Examens";
            Ds = new DataSet();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                adapter.InsertCommand = new SqlCommand("sp_InsertExamen", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@subject", SqlDbType.Text, 0, "Subject"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 50, "Password"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@procent_3", SqlDbType.Int, 0, "Procent_3"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@procent_4", SqlDbType.Int, 0, "Procent_4"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@procent_5", SqlDbType.Int, 0, "Procent_5"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@amountTasks", SqlDbType.Int, 0, "AmountTasks"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@timeExamen", SqlDbType.Int, 0, "TimeExamen"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                connection.Open();
                adapter.Fill(Ds);
                examensTable = Ds.Tables[0];

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
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.Update(examensTable);        
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
                    DataRow newRow = examensTable.NewRow();
                    newRow["Subject"] = "Новый экзамен";
                    newRow["AmountTasks"] = 0;
                    newRow["TimeExamen"] = 0;
                    examensTable.Rows.Add(newRow);

                    adapter.Update(Ds);
                    Ds.AcceptChanges();

                    //EditWindow = new EditExamenWindow(examensTable);
                    //EditWindow.ShowDialog();
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
                    //PasswordWindow = new PasswordWindow(SelectedExamen, examensTable);
                    //PasswordWindow.ShowDialog();
                }/*, (obj) => SelectedExamen != null*/));
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
