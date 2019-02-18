using Examenator.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Classes
{
    public class TextTask : BaseTask
    {
        public TextTask()
        {
            Question = "";
            Answers = new ObservableCollection<TextAnswer>();
            Answers.Add(new TextAnswer() { ValueAnswer = "" } );
            Answers.Add(new TextAnswer() { ValueAnswer = "" } );
            Answers.Add(new TextAnswer() { ValueAnswer = "" } );
            Answers.Add(new TextAnswer() { ValueAnswer = "" } );
        }

        override public void Assign(BaseTask task)
        {
            base.Assign(task);
        }

        override public BaseTask Clone()
        {
            var cloned = (TextTask)Activator.CreateInstance(GetType());
            cloned.Assign(this);
            return cloned;
        }
    }
}
