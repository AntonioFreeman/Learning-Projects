using Examenator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.AbstractClases
{
    public abstract class BaseExamen : IExam 
    {
        public string Subject { get; set; }

        public List<BaseTask> Tasks { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
