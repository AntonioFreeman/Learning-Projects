using Examenator.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Examenator.Views
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        private DataRow currentExamen;
        private DataSet dataSet;
        private SqlDataAdapter aT;
        private int indexExamen;
        public PasswordWindow(DataRow examen, DataSet ds, SqlDataAdapter adapterTasks)
        {
            InitializeComponent();
            currentExamen = examen;
            dataSet = ds;
            aT = adapterTasks;
            indexExamen = ds.Tables[0].Rows.IndexOf(examen);
        }
        public void Verification(object sender, RoutedEventArgs e)
        {
            if((string)currentExamen["Password"] == pswrd_1.Password)
            {
                var editWindow = new EditExamenWindow(indexExamen, dataSet, aT);
                editWindow.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Введен неверный пароль");
            }
        }
    }
}
