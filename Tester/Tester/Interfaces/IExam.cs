using Examenator.AbstractClases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Interfaces
{
    interface IExam<T,K> : IEnumerable
    {
        string Subject { get; set; }
        int AmountTask { get; set; }

        List<BaseTask<T,K>> Tasks { get; set; }
    }
}
