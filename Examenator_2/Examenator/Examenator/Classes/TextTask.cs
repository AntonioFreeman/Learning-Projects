using Examenator.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    }
}
