using Examenator.AbstractClases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Interfaces
{
    interface IExam : IEnumerable
    {
        string Subject { get; set; }
    }
}
