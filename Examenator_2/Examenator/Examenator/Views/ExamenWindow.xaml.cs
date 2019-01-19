using Examenator.Classes;
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
    /// Interaction logic for ExamenWindow.xaml
    /// </summary>
    public partial class ExamenWindow : Window
    {
        public ExamenWindow(Examen examen)
        {
            InitializeComponent();
        }
    }
}
