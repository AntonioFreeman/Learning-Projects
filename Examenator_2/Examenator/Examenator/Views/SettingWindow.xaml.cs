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
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private Exam currentExam;
        public SettingWindow(Exam currentExam)
        {
            InitializeComponent();
            this.currentExam = currentExam;
            var settingVM = new SettingViewModel(currentExam);
            DataContext = settingVM;            
            settingVM.CloseHandler += (sender, args) => this.Close(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            currentExam.Password = pswrd.Password;
        }
    }
}
