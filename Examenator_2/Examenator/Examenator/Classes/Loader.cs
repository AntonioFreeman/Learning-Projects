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
        public SqlConnection Connection { get; private set; }
        public SqlDataAdapter AdapterExamens { get; private set; }
        public SqlDataAdapter AdapterTasks { get; private set; }

        public Loader()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Connection = new SqlConnection(connectionString);
                                           
            SqlCommand commandLoadExamens = new SqlCommand("SELECT * FROM Examens", Connection);
            AdapterExamens = new SqlDataAdapter(commandLoadExamens);
            AdapterExamens.DeleteCommand = new SqlCommand("DELETE FROM Examens WHERE Id = @Id; DELETE FROM Tasks WHERE Id_Examen = @Id", Connection);
            AdapterExamens.DeleteCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 0, "Id"));
            AdapterExamens.InsertCommand = new SqlCommand("sp_InsertExamen", Connection);           
            AdapterExamens.InsertCommand.CommandType = CommandType.StoredProcedure;
            AdapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@subject", SqlDbType.Text, 0, "Subj"));
            AdapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 50, "Pswrd"));
            AdapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@procent_3", SqlDbType.Int, 0, "Procent_3"));
            AdapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@procent_4", SqlDbType.Int, 0, "Procent_4"));
            AdapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@procent_5", SqlDbType.Int, 0, "Procent_5"));
            AdapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@amountTasks", SqlDbType.Int, 0, "AmountTasks"));
            AdapterExamens.InsertCommand.Parameters.Add(new SqlParameter("@timeExamen", SqlDbType.Int, 0, "TimeExamen"));
            SqlParameter parameter = AdapterExamens.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            parameter.Direction = ParameterDirection.Output;

            SqlCommand commandLoadTasks = new SqlCommand("SELECT * FROM Tasks", Connection);
            AdapterTasks = new SqlDataAdapter(commandLoadTasks);
            AdapterTasks.DeleteCommand = new SqlCommand("DELETE FROM Tasks WHERE Id = @Id", Connection);
            AdapterTasks.DeleteCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 0, "Id"));
            AdapterTasks.InsertCommand = new SqlCommand("sp_InsertTask", Connection);
            AdapterTasks.InsertCommand.CommandType = CommandType.StoredProcedure;
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@title", SqlDbType.Text, 0, "Title"));
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@question", SqlDbType.Text, 50, "Question"));
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@answer_1", SqlDbType.Text, 0, "Answer_1"));
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@correct_Ans_1", SqlDbType.Bit, 0, "Correct_Ans_1"));
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@answer_2", SqlDbType.Text, 0, "Answer_2"));
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@correct_Ans_2", SqlDbType.Bit, 0, "Correct_Ans_2"));
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@answer_3", SqlDbType.Text, 0, "Answer_3"));
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@correct_Ans_3", SqlDbType.Bit, 0, "Correct_Ans_3"));
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@answer_4", SqlDbType.Text, 0, "Answer_4"));
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@correct_Ans_4", SqlDbType.Bit, 0, "Correct_Ans_4"));
            AdapterTasks.InsertCommand.Parameters.Add(new SqlParameter("@id_Examen", SqlDbType.Int, 0, "Id_Examen"));
            SqlParameter parameterId = AdapterTasks.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
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
                    if (Connection != null)
                        Connection.Close();
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
                if (Connection != null)
                    Connection.Close();
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
