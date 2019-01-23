using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Interfaces
{
    interface IAnswer : INotifyPropertyChanged
    {
        bool Correct { get; set; }
        bool Check { get; set; }
    }
}
