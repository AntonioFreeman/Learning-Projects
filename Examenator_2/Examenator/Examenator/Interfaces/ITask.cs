using Examenator.AbstractClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Interfaces
{
    interface ITask : INotifyPropertyChanged
    {
        string Title { get; set; }
        string Question { get; set; }
        List<BaseAnswer> Answers { get; set; }
    }
}
