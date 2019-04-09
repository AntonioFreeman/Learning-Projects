using Examenator.Classes;
using Examenator.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EnterNameWindow.xaml
    /// </summary>
    public partial class EnterNameWindow : Window
    {
        public EnterNameWindow(Result result, Exam exam)
        {
            InitializeComponent();
            var enterNameVM = new EnterNameViewModel(result, exam);
            DataContext = enterNameVM;
            enterNameVM.CloseHandler += (sender, args) => this.Close();
        }
    }
}
