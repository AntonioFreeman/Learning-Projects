using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Interfaces
{
    public interface ITask
    {
        string NumberOfTask { get; set; }
        string Question { get; set; }
        List<BaseAnswer> Answers { get; set;}
    }
}
