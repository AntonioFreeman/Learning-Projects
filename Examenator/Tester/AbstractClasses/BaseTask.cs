using Examenator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.AbstractClases
{
    public abstract class BaseTask : ITask
    {
        public string Question { get; set; }
        public List<BaseAnswer> Answers { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}


