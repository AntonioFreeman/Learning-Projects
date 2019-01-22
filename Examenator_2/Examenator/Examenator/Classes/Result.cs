﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Classes
{
    public class Result
    {
        public int AmountTask { get; set; }
        public TimeSpan TimeExecute { get; set; }
        public int CorrectAnswers { get; set; }
        public int UncorrectAnswers { get; set; }

        public Result(int amountTask)
        {
            AmountTask = amountTask;
        }
    }
}
