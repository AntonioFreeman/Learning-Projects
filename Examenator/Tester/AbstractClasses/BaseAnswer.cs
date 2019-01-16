using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examenator.Interfaces;

namespace Examenator
{
    public abstract class BaseAnswer : IAnswer
    {
        public bool Correct { get; set; }
        public bool Cheked { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
