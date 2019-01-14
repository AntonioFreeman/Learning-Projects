using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class Question : IEnumerable
    {
        private string textQuestion;
        private List<string> answers;



        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
