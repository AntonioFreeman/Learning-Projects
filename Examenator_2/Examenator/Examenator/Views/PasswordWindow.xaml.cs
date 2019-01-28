using Examenator.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Examen currentExamen;
        private ObservableCollection<Examen> editExamens;
        public PasswordWindow(Examen examen, ObservableCollection<Examen> examens)
        {
            InitializeComponent();
            currentExamen = examen;
            editExamens = examens;
        }
        public void Verification(object sender, RoutedEventArgs e)
        {
            if(currentExamen.Password == pswrd_1.Password)
            {
                var editWindow = new EditExamenWindow(currentExamen, editExamens);
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
