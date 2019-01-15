using Examenator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.AbstractClases
{
    public abstract class BaseTask<T, K> : ITask<T, K>
    {
        public T Question { get; set; }
        public List<BaseAnswer<K>> Answers { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}


