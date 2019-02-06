using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Interfaces
{
    interface IExam : INotifyPropertyChanged
    {
        int Id { get; set; }
        string Subject { get; set; }
    }
}
