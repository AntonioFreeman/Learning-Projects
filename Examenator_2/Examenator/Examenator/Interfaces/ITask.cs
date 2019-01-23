using Examenator.AbstractClasses;
using Examenator.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Interfaces
{
    interface ITask : INotifyPropertyChanged
    {
        string Title { get; set; }
        string Question { get; set; }
        ObservableCollection<TextAnswer> Answers { get; set; }
    }
}
