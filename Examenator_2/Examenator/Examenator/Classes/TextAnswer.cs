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

        override public void Assign(BaseAnswer answer)
        {
            base.Assign(answer);
            ValueAnswer = ((TextAnswer)answer).ValueAnswer;
        }

        override public BaseAnswer Clone()
        {
            var cloned = (TextAnswer)Activator.CreateInstance(GetType());
            cloned.Assign(this);
            return cloned;
        }
    }
}
