﻿using System;
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
        string Subject { get; set; }
    }
}
