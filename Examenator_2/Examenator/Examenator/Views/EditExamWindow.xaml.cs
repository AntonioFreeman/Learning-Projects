using Examenator.Classes;
using Examenator.ViewModels;
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
    public partial class EditExamWindow : Window
    {
        public EditExamWindow(int selectedExam, DataTable et, Loader loader)
        {
            InitializeComponent();
            DataContext = new EditExamViewModel(et, selectedExam, loader);
        }

        public EditExamWindow(DataTable dt, Loader loader)
        {
            InitializeComponent();
            DataContext = new EditExamViewModel(dt, loader);
        }
    }
}
