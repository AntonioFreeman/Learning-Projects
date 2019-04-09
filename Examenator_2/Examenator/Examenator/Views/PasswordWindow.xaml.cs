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
        private DataRow currentExam;
        private DataTable examsTable;
        private Loader loader;
        public PasswordWindow(DataRow currentExam, DataTable examsTable, Loader loader)  
        {
            InitializeComponent();
            this.currentExam = currentExam;
            this.examsTable = examsTable;
            this.loader = loader;
        }
        public void Verification(object sender, RoutedEventArgs e)
        {
            if((string)currentExam["Password"] == pswrd_1.Password)
            {
                var editWindow = new EditExamWindow((int)currentExam["Id"], examsTable, loader);
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
