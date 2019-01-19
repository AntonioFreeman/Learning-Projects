using Examenator.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Classes
{
    public class TextAnswer : BaseAnswer
    {
        private string valueAnswer;
        public string ValueAnswer
        {
            get { return valueAnswer; }
            set
            {
                valueAnswer = value;
                OnPropertyChanged("ValueAnswer");
            }
        }
    }
}
