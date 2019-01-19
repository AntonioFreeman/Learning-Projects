using Examenator.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Classes
{
    public class TextTask : BaseTask
    {
        public TextTask()
        {
            Question = "1";
            Answers = new List<BaseAnswer>();
            Answers.Add(new TextAnswer() { ValueAnswer = "2" } );
            Answers.Add(new TextAnswer() { ValueAnswer = "3" } );
            Answers.Add(new TextAnswer() { ValueAnswer = "4" } );
            Answers.Add(new TextAnswer() { ValueAnswer = "5" } );
        }
    }
}
