using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Examenator.Classes
{
    public class Loader
    {
        private string textForSave;
        private string connectionString;
        private SqlConnection connection;
        public SqlDataAdapter adapterExamens { get; private set; }
        public SqlDataAdapter adapterTasks { get; private set; }

        public Loader()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
                                           
            SqlCommand commandLoadExamens = new SqlCommand("SELECT * FROM Examens", connection);
            adapterExamens = new SqlDataAdapter(commandLoadExamens);
            adapterExamens.DeleteCommand = new SqlCommand("DELETE FROM Examens WHERE Id = @Id; DELETE FROM Tasks WHERE Id_Examen = @Id", connection);
            adapterExamens.DeleteCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 0, "Id"));
            adapterExamens.InsertCommand = new SqlCommand("sp_InsertExamen", connection);           
            adapterExamens.InsertCommand.CommandType = CommandType.StoredProcedure;
            adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@subject", SqlDbType.Text, 0, "Subj"));
            adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 50, "Pswrd"));
            adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@procent_3", SqlDbType.Int, 0, "Procent_3"));
            adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@procent_4", SqlDbType.Int, 0, "Procent_4"));
            adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@procent_5", SqlDbType.Int, 0, "Procent_5"));
            adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@amountTasks", SqlDbType.Int, 0, "AmountTasks"));
            adapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@timeExamen", SqlDbType.Int, 0, "TimeExamen"));
            SqlParameter parameter = adapterExamens.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            parameter.Direction = ParameterDirection.Output;

            SqlCommand commandLoadTasks = new SqlCommand("SELECT * FROM Tasks", connection);
            adapterTasks = new SqlDataAdapter(commandLoadTasks);
            adapterTasks.DeleteCommand = new SqlCommand("DELETE FROM Tasks WHERE Id = @Id", connection);
            adapterTasks.DeleteCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 0, "Id"));
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
            SqlParameter parameterId = adapterTasks.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            parameterId.Direction = ParameterDirection.Output;
        }

        public DataSet Load(SqlDataAdapter adapter)
            {
                try
                { 
                    var newDataSet = new DataSet();
                    adapter.Fill(newDataSet);
                    return newDataSet;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }

        public void Save(SqlDataAdapter adapter, DataTable dataTable)
        {
            try
            {
                adapter.Update(dataTable);
                dataTable.AcceptChanges();
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
        }

        public Loader(String text)
        {
            textForSave = text;
        }

        public void SaveTextFile()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; 
            dlg.DefaultExt = ".text"; 
            dlg.Filter = "Text documents (.txt)|*.txt"; 

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, false))
                {
                    file.WriteLine(textForSave);
                }
            }
        }
    }
}
