using Examenator.Clases;
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
    /// Interaction logic for EditExamenWindow.xaml
    /// </summary>
    public partial class EditExamenWindow : Window
    {
        public Examen Examen { get; set; }

        public EditExamenWindow(Examen ex)
        {            
            InitializeComponent();
            Examen = ex;
            this.DataContext = new EditExamenViewModel(Examen);
        }

    }
}
