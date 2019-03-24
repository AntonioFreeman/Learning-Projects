using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebExamenator.Models
{
    public class TextTask
    {
        public int Id { get; set; }
        public int IdExam { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public bool CorrectAns1 { get; set; }
        public string Answer2 { get; set; }
        public bool CorrectAns2 { get; set; }
        public string Answer3 { get; set; }
        public bool CorrectAns3 { get; set; }
        public string Answer4 { get; set; }
        public bool CorrectAns4 { get; set; }
    }
}