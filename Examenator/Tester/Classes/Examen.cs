﻿using Examenator.AbstractClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Clases
{
    public class Examen : BaseExamen
    {
        public int AmountTask { get; set; }
        public TimeSpan TimeExamen { get; set; } 
    }
}
