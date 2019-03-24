using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebExamenator.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public int Password { get; set; }
        public int Procent3 { get; set; }
        public int Procent4 { get; set; }
        public int Procent5 { get; set; }
        public int AmountTask { get; set; }
        public int TimeExamen { get; set; }
    }
}