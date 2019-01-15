using Examenator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.AbstractClases
{
    public abstract class BaseExamen <T,K> : IExam <T,K>
    {
        public string Subject { get; set; }
        public int AmountTask { get; set; }

        public List<BaseTask<T, K>> Tasks { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
