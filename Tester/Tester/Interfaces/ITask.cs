using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Interfaces
{
    public interface ITask<T, K> : IEnumerable
    {
        T Question { get; set; }
        List<BaseAnswer<K>> Answers { get; set;}
    }
}
