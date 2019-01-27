using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Classes
{
    public class Result
    {       
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int AmountTask { get; set; }
        public TimeSpan TimeExecute { get; set; }
        public int CorrectAnswers { get; set; }
        public int UncorrectAnswers { get; set; }
        public int Procent_3 { get; set; }
        public int Procent_4 { get; set; }
        public int Procent_5 { get; set; }

        public Result(Examen examen)
        {
            FirstName = "";
            SecondName = "";
            AmountTask = examen.AmountTask;
            Procent_3 = examen.Procent_3;
            Procent_4 = examen.Procent_4;
            Procent_5 = examen.Procent_5;
        }
    }
}
